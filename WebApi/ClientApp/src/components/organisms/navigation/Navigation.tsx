import React, { PropsWithChildren, useContext } from 'react';
import * as Styled from './Navigation.styles';
import { WorldContext } from 'contexts/WorldContext';
const Navigation = (props: PropsWithChildren<{}>) => {
  const { worlds: list, selected } = useContext(WorldContext);
  return (
    <Styled.Navigation_Container>
      <Styled.Container>
        <Styled.NavigationLink to="/">
          <Styled.App_Title>Tw Helper</Styled.App_Title>
        </Styled.NavigationLink>
      </Styled.Container>
      {selected != null ? (
        <Styled.Links_Container>
          <Styled.NavigationLink to={`/${selected.SubDomain}`}>
            <Styled.Item>Informacje</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={`/${selected.SubDomain}/rank`}>
            <Styled.Item>Ranking</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={`/${selected.SubDomain}/map`}>
            <Styled.Item>Mapa</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={'/foo'}>
            <Styled.Item>404 level 1</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={`/${selected.SubDomain}/foo`}>
            <Styled.Item>404 level 2</Styled.Item>
          </Styled.NavigationLink>
        </Styled.Links_Container>
      ) : (
        <Styled.Links_Container>
          {list.length > 0 && list.map((x) => {
            return <Styled.NavigationLink key={x.Id} to={`/${x.SubDomain}`}>
                <Styled.Item>{x.Name}</Styled.Item>
              </Styled.NavigationLink>
          })}
        </Styled.Links_Container>
      )}
    </Styled.Navigation_Container>
  );
};

export default Navigation;
