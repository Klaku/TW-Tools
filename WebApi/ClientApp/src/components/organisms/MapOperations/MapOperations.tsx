import { ColorPicker, ComboBox, DefaultButton, IColor, IComboBoxOption, PrimaryButton } from '@fluentui/react';
import { TribeContext } from 'contexts/TribeContext';
import React, { useContext } from 'react';
import styled from 'styled-components';
import { Tribe } from 'types/Tribe';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { GroupOptions } from 'components/molecules/GroupOptions';
export interface IGroup {
  color: IColor;
  key: string;
  tribes: Tribe[];
}

export const MapOperations = (props: { groups: IGroup[]; setGroups: (groups: IGroup[]) => void }) => {
  const Context = {
    Tribe: useContext(TribeContext),
  };

  const UpdateColorByKey = (key: string, color: IColor) => {
    let state = props.groups;
    state = state.map((x) => {
      if (x.key == key) {
        x.color = color;
      }
      return x;
    });
    props.setGroups(state);
  };

  const PushTribe = (key: string, tribe: Tribe) => {
    let state = props.groups;
    state = state.map((x) => {
      if (x.key == key) {
        x.tribes.push(tribe);
      }
      return x;
    });
    props.setGroups(state);
  };

  const RemoveTribe = (key: string, tribe: Tribe) => {
    let state = props.groups;
    state = state.map((x) => {
      if (x.key == key) {
        x.tribes = x.tribes.filter((x) => x.Id != tribe.Id);
      }
      return x;
    });
    props.setGroups(state);
  };

  return (
    <Wrapper>
      {props.groups.map((x, i) => {
        return <GroupOptions key={x.key} UpdateColorByKey={UpdateColorByKey} item={x} index={i} PushTribe={PushTribe} RemoveTribe={RemoveTribe} />;
      })}
    </Wrapper>
  );
};

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  max-width: calc(100% - 615px);
`;

const ButtonContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: center;
  width: 100%;
`;
