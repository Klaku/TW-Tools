import React, { PropsWithChildren } from "react";
import * as Styled from "./Navigation.styles";
import { NavLink } from "react-router-dom";
import NavigationFooter from "../navigationFooter/NavigationFooter";
const Navigation = (props: PropsWithChildren<{}>) => {
  return (
    <Styled.Navigation_Container>
      <Styled.Container>
        <Styled.NavigationLink to="/">
          <Styled.App_Title>Tw Helper</Styled.App_Title>
        </Styled.NavigationLink>
      </Styled.Container>
      <Styled.Links_Container>
        <Styled.NavigationLink to={"/plan"}>
          <Styled.Item>Planer Ataków</Styled.Item>
        </Styled.NavigationLink>
        <Styled.NavigationLink to={"/foo1"}>
          <Styled.Item>Kalkulator #1</Styled.Item>
        </Styled.NavigationLink>
        <Styled.NavigationLink to={"/foo2"}>
          <Styled.Item>Kalkulator #2</Styled.Item>
        </Styled.NavigationLink>
        <Styled.NavigationLink to={"/foo3"}>
          <Styled.Item>Kalkulator #3</Styled.Item>
        </Styled.NavigationLink>
      </Styled.Links_Container>
      <Styled.Container><NavigationFooter /></Styled.Container>
    </Styled.Navigation_Container>
  );
};

export default Navigation;
