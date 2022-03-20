import React, { PropsWithChildren, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

export interface IWorldContext {
  list: IWorld[];
  selected: IWorld | null;
  setSelectedWorld: (world: IWorld | null) => void;
  fetching: boolean;
}
export interface IWorld {
  id: number;
  name: string;
  domain: string;
  subDomain: string;
}
const DefaultContextValue: IWorldContext = {
  list: [],
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
          setSelectedWorld(data.find((x) => x.subDomain == params.world) || null);
        }
        setFetching(false);
      });
  }, []);
  return (
    <WorldContext.Provider
      value={{
        list: worlds,
        selected: selectedWorld,
        setSelectedWorld,
        fetching,
      }}>
      {props.children}
    </WorldContext.Provider>
  );
};
