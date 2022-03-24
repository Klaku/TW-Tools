import Navigation from 'components/organisms/navigation/Navigation';
import React, { PropsWithChildren, useContext } from 'react';
import * as Styled from './Main.styles';
import { Routes, Route } from 'react-router-dom';
import Panel from 'components/templates/Panel/PanelWrapper';
import World from 'components/templates/World/WorldWrapper';
import Home from 'views/Home/App/Home';
import NotFound from 'views/NotFound/NotFound';
import { WorldContext } from 'contexts/WorldContext';
import Map from 'views/Map/Map';
import { Spinner } from '@fluentui/react';
import WorldHome from 'views/Home/World/Home';
import Ranking from 'views/Ranking/Ranking';
const Main = (props: PropsWithChildren<{}>) => {
  const Context = {
    World: useContext(WorldContext),
  };

  if (Context.World.fetching) {
    return (
      <Styled.PlaceholderWrapper>
        <Styled.SpinnerWrapper>
          <Spinner label="Wczytywanie danych" />
        </Styled.SpinnerWrapper>
      </Styled.PlaceholderWrapper>
    );
  }

  return (
    <Styled.Grid_Layout>
      <Styled.Navigation_Container>
        <Navigation />
      </Styled.Navigation_Container>
      <Styled.Content_Container>
        <Routes>
          <Route path="/" element={<Panel />}>
            <Route index element={<Home />} />
            <Route path="404" element={<NotFound />} />
            <Route path=":world" element={<World />}>
              <Route index element={<WorldHome />} />
              <Route path="rank" element={<Ranking />}>
                <Route path=":type" element={<Ranking />}>
                  <Route path=":sort" element={<Ranking />}>
                    <Route path=":method" element={<Ranking />} />
                  </Route>
                </Route>
              </Route>
              <Route path="map" element={<Map />} />
              <Route path="*" element={<NotFound />} />
            </Route>
          </Route>
        </Routes>
      </Styled.Content_Container>
    </Styled.Grid_Layout>
  );
};

export default Main;
