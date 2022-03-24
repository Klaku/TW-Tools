import React, { PropsWithChildren, useContext, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Player } from '../types/Player';
import { WorldContext } from './WorldContext';

export interface IPlayerContext {
  players: Player[];
  fetching: boolean;
  fetchPlayers: () => void;
}

const DefaultContextValue: IPlayerContext = {
  players: [],
  fetching: true,
  fetchPlayers: () => {},
};

export const PlayerContext = React.createContext(DefaultContextValue);

export const PlayerContextWrapper = (props: PropsWithChildren<{}>) => {
  const [players, setPlayers] = useState([] as Player[]);
  const [fetching, setFetching] = useState(true);
  const context = useContext(WorldContext);
  const fetchPlayers = () => {
    if (context.selected != null) {
      setFetching(true);
      fetch(`/api/Player/GetPlayers?worldId=${context.selected?.id}`)
        .then((response) => response.json())
        .then((data: Player[]) => {
          setPlayers(data);
          setFetching(false);
        });
    }
  };
  useEffect(fetchPlayers, []);
  return <PlayerContext.Provider value={{ players, fetching, fetchPlayers }}>{props.children}</PlayerContext.Provider>;
};
