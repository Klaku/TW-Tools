import ValueColor from 'components/atoms/ValueColor';
import React, { PropsWithChildren } from 'react';
import styled from 'styled-components';

const RankingItemDetails = (
  props: PropsWithChildren<{
    value: number;
    value7: number;
    value24: number;
    value30: number;
  }>
) => {
  const { value, value24, value30, value7 } = props;
  return (
    <Cell>
      <CellRow>
        <div>Aktualnie</div>
        <div>{Number(value).toLocaleString('de')}</div>
      </CellRow>
      <CellRow>
        <div>24 godziny</div>
        <ValueColor value={value - value24}>{Number(Math.abs(value24 - value)).toLocaleString('de')}</ValueColor>
      </CellRow>
      <CellRow>
        <div>7 dni</div>
        <ValueColor value={value - value7}>{Number(Math.abs(value7 - value)).toLocaleString('de')}</ValueColor>
      </CellRow>
      <CellRow>
        <div>30 dni</div>
        <ValueColor value={value - value30}>{Number(Math.abs(value30 - value)).toLocaleString('de')}</ValueColor>
      </CellRow>
    </Cell>
  );
};

export const Cell = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
`;

export const CellRow = styled.div`
  display: flex;
  width: 100%;
  min-width: 160px;
  flex-direction: row;
  justify-content: center;
  & > div:first-child {
    padding: 0 5px 0 10px;
    min-width: 26px;
    white-space: nowrap;
    word-break: keep-all;
    text-align: right;
    width: 50%;
  }
  & > div:last-child {
    padding: 0 10px 0 5px;
    white-space: nowrap;
    word-break: keep-all;
    text-align: left;
    min-width: 26px;
    width: 50%;
  }
`;

export default RankingItemDetails;
