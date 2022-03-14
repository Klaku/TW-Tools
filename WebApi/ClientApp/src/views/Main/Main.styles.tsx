import styled from "styled-components";

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
