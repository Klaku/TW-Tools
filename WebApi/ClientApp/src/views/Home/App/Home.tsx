import { WorldContext } from 'contexts/WorldContext';
import React, { PropsWithChildren, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import * as Styled from './Home.styles';
import { DefaultButton } from '@fluentui/react';
const Home = (props: PropsWithChildren<{}>) => {
  const { worlds: list, setSelectedWorld } = useContext(WorldContext);
  const navigate = useNavigate();

  return (
    <Panel.Wrapper>
      <Panel.Title>Tw Helper</Panel.Title>
      <Panel.Section>
        <Panel.Title2>Dostępne światy</Panel.Title2>
        <Styled.WorldListWrapper>
          {list.map((x) => {
            return (
              <DefaultButton
                onClick={() => {
                  setSelectedWorld(x);
                  navigate(x.subDomain);
                }}
                key={x.id}>
                {x.name}
              </DefaultButton>
            );
          })}
        </Styled.WorldListWrapper>
      </Panel.Section>
      <Panel.Section>
        <Panel.Title2>Kilka słów od autora</Panel.Title2>
        <p>Aplikacja tworzona przy współpracy z plemieniem Klasyk, wspólnymi siłami ku zwycięstwu.</p>
        <p>
          <a href="https://github.com/klaku">Autor</a> <br></br>
          <a href="https://github.com/Klaku/TW-Tools">Code Repo</a>
        </p>
      </Panel.Section>
    </Panel.Wrapper>
  );
};

export default Home;
