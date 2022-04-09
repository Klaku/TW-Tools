import React, { PropsWithChildren, useContext, useState } from 'react';
import { AnyStyledComponent } from 'styled-components';
import { Village } from 'types/Village';
import { WorldContext } from './WorldContext';

export interface IVillageContext {
  villages_tribe: Village[];
  TribeVillages_fetch: (id: number) => void;
  TribeVillages_remove: (id: number) => void;
  TribeVillages_clear: () => void;
}
const DefaultContextValue: IVillageContext = {
  villages_tribe: [],
  TribeVillages_fetch: () => {},
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
  return <VillageContext.Provider value={{ villages_tribe, TribeVillages_fetch, TribeVillages_clear, TribeVillages_remove }}>{props.children}</VillageContext.Provider>;
};
