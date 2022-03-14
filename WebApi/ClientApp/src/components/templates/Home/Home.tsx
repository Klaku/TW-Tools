import { WorldContext } from "contexts/WorldContext";
import React, { PropsWithChildren, useContext } from "react";
import { Wrapper } from "./Home.styles";

const Home = (props: PropsWithChildren<{}>) => {
  const context = useContext(WorldContext);
  if (context.selected == null) {
    return (
      <Wrapper>
        <h1>Cześć, zacznij od wyboru jednego ze światów.</h1>
      </Wrapper>
    );
  }
  return (
    <Wrapper>
      <h1>Witamy na świecie, {context.selected.subDomain}</h1>
    </Wrapper>
  );
};

export default Home;
