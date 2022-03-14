import { getTheme } from "@fluentui/react";
import styled from "styled-components";
const theme = getTheme();
export const Component_Wrapper = styled.div`
    padding: 25px;
    margin: 25px;
    max-height: calc(100vh - 100px);
    min-height: calc(100vh - 100px);
    box-shadow: ${theme.effects.elevation64};
`