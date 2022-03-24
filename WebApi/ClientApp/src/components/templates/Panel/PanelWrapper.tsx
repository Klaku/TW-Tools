import React, { PropsWithChildren } from 'react';
import * as Styled from './PanelWrapper.styles';
import { Outlet } from 'react-router-dom';
const Panel = (props: PropsWithChildren<{}>) => {
  return (
    <Styled.Component_Wrapper>
      <Outlet />
    </Styled.Component_Wrapper>
  );
};

export default Panel;
