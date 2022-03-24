import React, { PropsWithChildren } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
const NotFound = (props: PropsWithChildren<{}>) => {
  return (
    <Panel.Wrapper>
      <Panel.Title>Oops!</Panel.Title>
      <Panel.Section>
        <Panel.Title2>Dzielnie szukałem ale się nie udało.</Panel.Title2>
        <div>Wróć do poprzedniej strony i obyśmy się tutaj ponownie nie spotkali.</div>
        <Panel.DescriptionLabel>Error Code: 404</Panel.DescriptionLabel>
      </Panel.Section>
    </Panel.Wrapper>
  );
};

export default NotFound;
