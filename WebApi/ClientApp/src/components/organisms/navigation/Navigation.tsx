import React, { PropsWithChildren, useContext } from 'react';
import * as Styled from './Navigation.styles';
import { WorldContext } from 'contexts/WorldContext';
const Navigation = (props: PropsWithChildren<{}>) => {
  const { list, selected } = useContext(WorldContext);
  return (
    <Styled.Navigation_Container>
      <Styled.Container>
        <Styled.NavigationLink to="/">
          <Styled.App_Title>Tw Helper</Styled.App_Title>
        </Styled.NavigationLink>
      </Styled.Container>
      {selected != null ? (
        <Styled.Links_Container>
          <Styled.NavigationLink to={`/${selected.subDomain}`}>
            <Styled.Item>Informacje</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={`/${selected.subDomain}/rank`}>
            <Styled.Item>Ranking</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={'/foo'}>
            <Styled.Item>404 level 1</Styled.Item>
          </Styled.NavigationLink>
          <Styled.NavigationLink to={`/${selected.subDomain}/foo`}>
            <Styled.Item>404 level 2</Styled.Item>
          </Styled.NavigationLink>
        </Styled.Links_Container>
      ) : (
        <Styled.Links_Container>
          {list.map((x) => {
            return (
              <Styled.NavigationLink key={x.id} to={`/${x.subDomain}`}>
                <Styled.Item>{x.name}</Styled.Item>
              </Styled.NavigationLink>
            );
          })}
        </Styled.Links_Container>
      )}
    </Styled.Navigation_Container>
  );
};

export default Navigation;
