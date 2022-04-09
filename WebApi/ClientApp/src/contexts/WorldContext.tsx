import React, { PropsWithChildren, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

export interface IWorldContext {
  worlds: IWorld[];
  selected: IWorld | null;
  setSelectedWorld: (world: IWorld | null) => void;
  fetching: boolean;
}
export interface IWorld {
  Id: number;
  Name: string;
  Domain: string;
  SubDomain: string;
}
const DefaultContextValue: IWorldContext = {
  worlds: [],
  selected: null,
  setSelectedWorld: () => {},
  fetching: true,
};
export const WorldContext = React.createContext(DefaultContextValue);

export const WorldContextWrapper = (props: PropsWithChildren<{}>) => {
  let [worlds, setWorlds] = useState([] as IWorld[]);
  let [fetching, setFetching] = useState(true);
  let [selectedWorld, setSelectedWorld] = useState(null as IWorld | null);
  const params = useParams();
  useEffect(() => {
    fetch('/api/World/GetWorldList')
      .then((response) => response.json())
      .then((data: IWorld[]) => {
        setWorlds(data);
        if (typeof params.world != 'undefined') {
          setSelectedWorld(data.find((x) => x.SubDomain == params.world) || null);
        }
        setFetching(false);
      });
  }, []);
  return (
    <WorldContext.Provider
      value={{
        worlds: worlds,
        selected: selectedWorld,
        setSelectedWorld,
        fetching,
      }}>
      {props.children}
    </WorldContext.Provider>
  );
};
