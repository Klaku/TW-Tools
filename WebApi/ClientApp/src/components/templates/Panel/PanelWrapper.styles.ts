import styled from 'styled-components';

export const Component_Wrapper = styled.div`
  max-height: 100vh;
  height: 100vh;
  min-height: calc(100vh - 50px);
  padding: 25px;
  overflow: auto;
`;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
  justify-content: flex-start;
  align-items: flex-start;
`;

export const Title = styled.div`
  font-size: 2rem;
  display: block;
  margin: 10px 0;
  width: 100%;
  font-weight: bold;
  padding: 10px 0;
  border-bottom: 1px solid #333;
`;

export const Title2 = styled.div`
  font-size: 1rem;
  display: block;
  margin: 5px 0;
  width: 100%;
  font-weight: bold;
  padding: 5px 0;
`;

export const DescriptionLabel = styled.div`
  font-size: 0.875rem;
  display: block;
  font-weight: bold;
  color: #999;
`;

export const Section = styled.div`
  padding: 10px;
  margin: 10px 0;
  box-shadow: 0 3.2px 7.2px 0 rgb(0 0 0 / 13%), 0 0.6px 1.8px 0 rgb(0 0 0 / 11%);
  display: flex;
  flex-direction: column;
  width: 100%;
  flex-grow: 1;
`;

export const SectionRow = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;
  gap: 10px;
`;
