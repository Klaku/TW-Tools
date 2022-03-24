import styled from 'styled-components';

export const Grid_Layout = styled.div`
  display: grid;
  grid-template-columns: 150px 1fr;
  grid-template-rows: 1fr;
`;
export const Navigation_Container = styled.div`
  grid-row: 1;
  grid-column: 1;
`;

export const Content_Container = styled.div`
  grid-row: 1;
  grid-column: 2;
`;

export const PlaceholderWrapper = styled.div`
  display: flex;
  width: 100vw;
  height: 100vh;
  flex-direction: row;
  justify-content: center;
  align-items: center;
`;
export const SpinnerWrapper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
`;
