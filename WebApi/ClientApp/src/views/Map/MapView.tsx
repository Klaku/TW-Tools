import React, { PropsWithChildren, useContext, useEffect, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import * as MapStyles from './MapView.styles';
import { Map } from 'components/organisms/Map/Map';
import { Village } from 'types/Village';
import { IGroup, MapOperations } from 'components/organisms/MapOperations/MapOperations';
import { DefaultButton, getColorFromString, IColor, PrimaryButton, Spinner } from '@fluentui/react';
import { WorldContext } from 'contexts/WorldContext';
import { Tribe } from 'types/Tribe';
const MapView = (props: PropsWithChildren<{}>) => {
  const [villages, setVillages] = useState([] as Village[]);
  const [groups, setGroups] = useState([] as IGroup[]);
  const [fetching, setFetching] = useState(false);
  const context = useContext(WorldContext);
  const FetchVillages = async () => {
    setFetching(true);
    try {
      setVillages([]);
      let tribes: { color: IColor; tribe: Tribe }[] = [];
      groups.forEach((t) => {
        t.tribes.forEach((x) => {
          tribes.push({ color: t.color, tribe: x });
        });
      });
      let array: Village[] = [];
      for (var i = 0; i < tribes.length; i++) {
        let response = await fetch(`/api/Village/GetVillagesByTribeId?worldId=${context.selected?.Id}&tribeId=${tribes[i].tribe.TribeId}`);
        let json = (await response.json()) as Village[];
        array = array.concat(json);
      }
      setVillages(array);
    } catch (exception) {
      console.log(exception);
    } finally {
      setFetching(false);
    }
  };
  return (
    <Panel.Wrapper>
      <Panel.Title>Mapa świata</Panel.Title>
      <MapStyles.Section>
        <PrimaryButton
          disabled={fetching}
          onClick={() => {
            FetchVillages();
          }}
          text="Generuj Mapę"></PrimaryButton>
        <DefaultButton
          text="Nowa Grupa"
          onClick={() => {
            setGroups(
              groups.concat([
                {
                  key: new Date().toLocaleTimeString(),
                  color: getColorFromString('#0000dd') as IColor,
                  tribes: [],
                },
              ])
            );
          }}
        />
        <DefaultButton
          text="Zapisz"
          onClick={() => {
            localStorage.setItem(`map_${context.selected?.SubDomain}`, JSON.stringify(groups));
          }}
        />
        <DefaultButton
          text="Wczytaj"
          onClick={() => {
            let value = localStorage.getItem(`map_${context.selected?.SubDomain}`);
            if (value != null) {
              setGroups(JSON.parse(value));
            }
          }}
        />
      </MapStyles.Section>
      {fetching && (
        <Panel.Section style={{ display: 'flex', flexDirection: 'row', justifyContent: 'center', width: '100%', margin: '10px 0' }}>
          <Spinner label="Performing akszyn" />
        </Panel.Section>
      )}
      <MapStyles.Section>
        <Map villages={villages} groups={groups} />
        <MapOperations groups={groups} setGroups={setGroups} />
      </MapStyles.Section>
    </Panel.Wrapper>
  );
};

export default MapView;
