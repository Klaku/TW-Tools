import React, { PropsWithChildren, useContext, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Tribe } from '../types/Tribe';
import { WorldContext } from './WorldContext';

export interface ITribeContext {
  tribes: Tribe[];
  fetching: boolean;
  fetchTribes: () => void;
}

const DefaultContextValue: ITribeContext = {
  tribes: [],
  fetching: true,
  fetchTribes: () => {},
};

export const TribeContext = React.createContext(DefaultContextValue);

export const TribeContextWrapper = (props: PropsWithChildren<{}>) => {
  const [tribes, setTribes] = useState([] as Tribe[]);
  const [fetching, setFetching] = useState(true);
  const context = useContext(WorldContext);
  const fetchTribes = () => {
    if (context.selected != null) {
      setFetching(true);
      fetch(`/api/Tribe/GetTribes?worldId=${context.selected?.Id}`)
        .then((response) => response.json())
        .then((data: Tribe[]) => {
          setTribes(data);
          setFetching(false);
        });
    }
  };
  useEffect(fetchTribes, []);
  return <TribeContext.Provider value={{ tribes, fetching, fetchTribes }}>{props.children}</TribeContext.Provider>;
};
