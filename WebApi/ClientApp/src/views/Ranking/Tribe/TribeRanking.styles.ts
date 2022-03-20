import styled from 'styled-components';

export const FooterContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: baseline;
  gap: 10px;
  & > * {
    width: 30%;
  }
`;

export const PaginationSizeContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  & > * {
    width: 70px;
  }
`;

export const PaginationContainer = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: baseline;
  gap: 10px;
  padding: 10px 0;
`;

export const PaginationNavItem = styled.span`
  font-weight: 600;
  cursor: pointer;
`;
