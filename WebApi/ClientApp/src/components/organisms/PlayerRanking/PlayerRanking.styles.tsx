import styled from 'styled-components';

export const ListWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  width: 100%;
  max-height: calc(100vh - 330px);
  overflow: auto;
`;
export const Item = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  justify-content: flex-start;
  &:hover {
    background-color: #00000010;
  }
`;
export const HeaderRow = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: flex-start;
  flex-wrap: nowrap;
  padding: 10px 0;
  border-bottom: 1px solid #00000030;
`;
export const DataContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-end;
  margin-bottom: 10px;
`;
export const DataRow = styled.div`
  display: flex;
  flex-direction: row;
  padding: 5px 0;
  border-bottom: 1px solid #00000030;
`;
export const DataItem = styled.div`
  display: flex;
  min-width: 100px;
  padding: 0 10px;
  box-sizing: content-box;
`;
export const Col = styled.div`
  display: flex;
  flex-direction: row;
  padding: 0 10px;
  box-sizing: content-box;
`;

export const ExpandContainer = styled.div`
  display: flex;
  flex-direction: row;
  padding: 0 10px;
  box-sizing: content-box;
  width: 20px;
`;
