import React, { PropsWithChildren, useContext, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { TribeContext } from 'contexts/TribeContext';
import styled from 'styled-components';
import { Checkbox } from '@fluentui/react';
import { Tribe } from 'types/Tribe';
const PlenerTribeSelector = (props: PropsWithChildren<{}>) => {
  const Context = {
    Tribe: useContext(TribeContext),
  };
  const [selectedTribes, setSelectedTribes] = useState([] as Tribe[]);
  return (
    <Panel.Section>
      <Panel.Title2>Wybierz strony konfliktu</Panel.Title2>
      <HorizontalContainer>
        <ListWrapper>
          {Context.Tribe.tribes.map((tribe) => {
            return (
              <Checkbox
                label={`${tribe.Name} [${tribe.Tag}]`}
                onChange={(ev, isChecked) => {
                  let list = selectedTribes.filter((x) => x.Id != tribe.Id);
                  if (isChecked) {
                    list.push(tribe);
                  }
                  setSelectedTribes(list);
                }}
              />
            );
          })}
        </ListWrapper>
      </HorizontalContainer>
    </Panel.Section>
  );
};

export const ListWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;
export const HorizontalContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  & > * {
    padding: 15px;
  }
`;

export default PlenerTribeSelector;
