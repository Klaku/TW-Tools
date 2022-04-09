import { ColorPicker, IColor, TextField } from '@fluentui/react';
import { IGroup } from 'components/organisms/MapOperations/MapOperations';
import React, { useContext, useState } from 'react';
import styled from 'styled-components';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { Tribe } from 'types/Tribe';
import { TribeContext } from 'contexts/TribeContext';
export const GroupOptions = (props: {
  item: IGroup;
  index: number;
  UpdateColorByKey: (key: string, color: IColor) => void;
  PushTribe: (key: string, tribe: Tribe) => void;
  RemoveTribe: (key: string, tribe: Tribe) => void;
}) => {
  const context = useContext(TribeContext);
  const [filter, setFilter] = useState('');
  const [colapsed, setColapsed] = useState(false);
  return (
    <GroupContainer style={{ borderLeft: `5px solid ${props.item.color.str}` }}>
      <GroupHeader
        onClick={() => {
          setColapsed(!colapsed);
        }}>
        Grupa #{props.index + 1}
      </GroupHeader>
      <GroupContent style={{ display: colapsed ? 'none' : 'flex' }}>
        <ColorPicker
          color={props.item.color}
          alphaType={'none'}
          onChange={(event, color) => {
            props.UpdateColorByKey(props.item.key, color);
          }}></ColorPicker>
        <TribeSelector>
          <Panel.Title2>Plemiona:</Panel.Title2>
          <TribeContainer>
            {props.item.tribes.map((x) => {
              return (
                <TribeObject
                  onClick={() => {
                    props.RemoveTribe(props.item.key, x);
                  }}
                  key={x.Id}>
                  {x.Tag}
                </TribeObject>
              );
            })}
          </TribeContainer>
          <SectionSpliter></SectionSpliter>
          <div style={{ marginBottom: '15px' }}>
            <TextField
              placeholder="Filtruj"
              value={filter}
              onChange={(e) => {
                setFilter((e.target as HTMLInputElement).value);
              }}
            />
          </div>
          <TribeContainer>
            {context.tribes
              .filter((x) => `${x.Tag} ${x.Name}`.toLocaleLowerCase().indexOf(filter.toLocaleLowerCase()) != -1)
              .sort((a, b) => {
                return a.Tag.toLocaleLowerCase() > b.Tag.toLocaleLowerCase() ? 1 : -1;
              })
              .map((x) => {
                return (
                  <TribeObject
                    onClick={() => {
                      props.PushTribe(props.item.key, x);
                    }}
                    key={x.Id}>
                    {x.Tag}
                  </TribeObject>
                );
              })}
          </TribeContainer>
        </TribeSelector>
      </GroupContent>
    </GroupContainer>
  );
};

const SectionSpliter = styled.hr`
  margin: 15px 0;
`;

const TribeObject = styled.div`
  padding: 5px;
  border: 1px solid #333;
  border-radius: 5px;
  text-align: center;
  cursor: pointer;
  margin: 5px;
`;

const TribeContainer = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;
  flex-wrap: wrap;
  justify-content: flex-start;
  max-height: 150px;
  overflow: auto;
`;

const TribeSelector = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  padding: 15px;
`;

const GroupHeader = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  cursor: pointer;
  font-size: 20px;
  font-weight: bold;
  padding-left: 15px;
`;

const GroupContent = styled.div`
  display: flex;
  flex-direction: row;
  gap: 15px;
  width: 100%;
  justify-content: flex-start;
`;

const GroupContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  margin-bottom: 15px;
`;
