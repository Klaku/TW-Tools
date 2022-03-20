import React, { PropsWithChildren } from 'react';
import * as Panel from 'components/templates/Panel/Panel.styles';
import { Outlet } from 'react-router-dom';
const Ranking = (props: PropsWithChildren<{}>) => {
  return (
    <Panel.Wrapper>
      <Panel.Title>Ranking</Panel.Title>
      <Outlet />
    </Panel.Wrapper>
  );
};

export default Ranking;
