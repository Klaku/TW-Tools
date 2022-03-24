import { Spinner } from '@fluentui/react';
import { PlayerContext } from 'contexts/PlayerContext';
import { TribeContext } from 'contexts/TribeContext';
import { WorldContext } from 'contexts/WorldContext';
import React, { PropsWithChildren, useContext, useEffect } from 'react';
import { Outlet, useNavigate, useParams } from 'react-router-dom';
import styled from 'styled-components';

const WorldWrapper = (props: PropsWithChildren<{}>) => {
  const params = useParams();
  const navigate = useNavigate();
  const Contexts = {
    World: useContext(WorldContext),
    Tribe: useContext(TribeContext),
    Player: useContext(PlayerContext),
  };
  useEffect(() => {
    if (typeof params.world != 'undefined') {
      let selectedWorld = Contexts.World.worlds.find((x) => x.subDomain == params.world) || null;
      Contexts.World.setSelectedWorld(selectedWorld);
      if (selectedWorld == null) {
        navigate('/404');
      }
    } else {
      Contexts.World.setSelectedWorld(null);
    }
  }, [params.world]);

  useEffect(() => {
    if (Contexts.World.selected != null) {
      Contexts.Tribe.fetchTribes();
      Contexts.Player.fetchPlayers();
    }
  }, [Contexts.World.selected]);

  if (Contexts.Tribe.fetching || Contexts.Player.fetching) {
    return (
      <Wrapper>
        <Spinner label="Ładowanie danych świata"></Spinner>
      </Wrapper>
    );
  } else {
    return (
      <div>
        <Outlet />
      </div>
    );
  }
};

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  justify-content: center;
  height: 100%;
`;

export default WorldWrapper;
