import Navigation from 'components/organisms/navigation/Navigation';
import React, { PropsWithChildren, useContext } from 'react';
import * as Styled from './Main.styles';
import { Routes, Route } from 'react-router-dom';
import Panel from 'components/templates/Panel/Panel';
import World from 'components/templates/World/World';
import Home from 'views/Home/Home';
import NotFound from 'views/NotFound/NotFound';
import { WorldContext } from 'contexts/WorldContext';
const Main = (props: PropsWithChildren<{}>) => {
  const { fetching } = useContext(WorldContext);
  if (fetching) {
    return <div>Wait</div>;
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
              <Route index element={<div>World Home</div>} />
              <Route
                path="rank"
                element={
                  <div>
                    <span>Ranking</span>
                  </div>
                }
              />
              <Route path="*" element={<NotFound />} />
            </Route>
          </Route>
        </Routes>
      </Styled.Content_Container>
    </Styled.Grid_Layout>
  );
};

export default Main;
