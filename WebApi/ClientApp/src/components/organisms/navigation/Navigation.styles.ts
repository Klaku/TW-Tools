import styled from "styled-components";
import { getTheme } from "@fluentui/react";
import { NavLink } from "react-router-dom";
const theme = getTheme();
export const Navigation_Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  background-color: ${theme.palette.neutralDark};
  height: 100vh;
  color: ${theme.palette.neutralLight}
`;

export const Item = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  justify-content: flex-end;
  padding: 15px;
  text-decoration: none;
  color: inherit;
  &:hover {
    background-color: ${theme.palette.neutralPrimary};
    cursor: pointer;
  }
`;

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: unset;
  width: 100%;
`;

export const Links_Container = styled(Container)`
  flex-grow: 1;
  margin-top: 25px;
`;
export const App_Title = styled.div`
  font-size: ${theme.fonts.xLarge.fontSize};
  font-weight: ${theme.fonts.xLarge.fontWeight};
  text-align: center;
  padding: 10px 0;
`;

export const NavigationLink = styled(NavLink)`
  color: inherit;
  text-decoration: none;
`;
