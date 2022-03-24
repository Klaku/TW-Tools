import React, { PropsWithChildren, useContext } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { WorldContext } from 'contexts/WorldContext';
import { TribeContext } from 'contexts/TribeContext';
const Home = (props: PropsWithChildren<{}>) => {
  const Contexts = {
    World: useContext(WorldContext),
    Tribe: useContext(TribeContext),
  };
  return (
    <Panel.Wrapper>
      <Panel.Title>{Contexts.World.selected?.name}</Panel.Title>
      <Panel.Section>
        <Panel.Title2>Aktualnie monitoruje:</Panel.Title2>
        <span>{Contexts.Tribe.tribes.length} plemion</span>
        <span>{Math.floor(Math.random() * 70000)} graczy</span>
      </Panel.Section>
    </Panel.Wrapper>
  );
};

export default Home;
