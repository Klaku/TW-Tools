import Navigation from "components/organisms/navigation/Navigation";
import React, { PropsWithChildren } from "react";
import * as Styled from "./Main.styles";
import { Routes, Route } from "react-router-dom";
import Panel from "components/templates/Panel/Panel";
import Home from "components/templates/Home/Home";
import Plan from "components/templates/Plan/Plan";
import NotFound from "components/templates/NotFound/NotFound";
const Main = (props: PropsWithChildren<{}>) => {
  return (
    <Styled.Grid_Layout>
      <Styled.Navigation_Container>
        <Navigation />
      </Styled.Navigation_Container>
      <Styled.Content_Container>
        <Routes>
          <Route path="/" element={<Panel />}>
              <Route index element={<Home />} />
              <Route path="plan" element={<Plan />} />
              <Route path="*" element={<NotFound />} />
          </Route>
        </Routes>
      </Styled.Content_Container>
    </Styled.Grid_Layout>
  );
};

export default Main;
