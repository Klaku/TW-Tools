import React, { PropsWithChildren, useContext, useState } from 'react';
import { AnyStyledComponent } from 'styled-components';
import { Village } from 'types/Village';
import { WorldContext } from './WorldContext';

export interface IVillageContext {
  villages_tribe: Village[];
  TribeVillages_fetch: (id: number) => void;
  TribeVillages_fetch_async: (ids: string[]) => void;
  TribeVillages_remove: (id: number) => void;
  TribeVillages_clear: () => void;
}
const DefaultContextValue: IVillageContext = {
  villages_tribe: [],
  TribeVillages_fetch: () => {},
  TribeVillages_fetch_async: () => {},
  TribeVillages_remove: () => {},
  TribeVillages_clear: () => {},
};

export const VillageContext = React.createContext(DefaultContextValue);

export const VillageContextWrapper = (props: PropsWithChildren<{}>) => {
  const [villages_tribe, setVillages_tribe] = useState([] as Village[]);
  const [fetching, setFetching] = useState(true);
  const context = useContext(WorldContext);
  const TribeVillages_remove = (tribeId: number) => {
    setVillages_tribe(villages_tribe.filter((x) => x.TribeId != tribeId));
  };
  const TribeVillages_clear = () => {
    setVillages_tribe([]);
  };
  const TribeVillages_fetch = (tribeId: number) => {
    if (context.selected != null) {
      setFetching(true);
      fetch(`/api/Village/GetVillagesByTribeId?worldId=${context.selected?.Id}&tribeId=${tribeId}`)
        .then((response) => response.json())
        .then((data: Village[]) => {
          setVillages_tribe(villages_tribe.concat(data));
        })
        .finally(() => {
          setFetching(false);
        });
    }
  };
  const TribeVillages_fetch_async = async (keys: string[]) => {
    if (context.selected != null) {
      setFetching(true);
      try {
        let array = [] as Village[];
        for (let i = 0; i < keys.length; i++) {
          let response = await fetch(`/api/Village/GetVillagesByTribeId?worldId=${context.selected?.Id}&tribeId=${keys[i]}`);
          let data = (await response.json()) as Village[];
          array = array.concat(data);
        }
        setVillages_tribe(array);
      } catch (exception) {
        console.log('TribeVillages Async fetch failed');
        console.error(exception);
      } finally {
        setFetching(false);
      }
    }
  };
  return (
    <VillageContext.Provider
      value={{
        TribeVillages_fetch_async,
        villages_tribe,
        TribeVillages_fetch,
        TribeVillages_clear,
        TribeVillages_remove,
      }}>
      {props.children}
    </VillageContext.Provider>
  );
};
