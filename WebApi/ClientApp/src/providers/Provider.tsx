import React, { PropsWithChildren } from 'react';
import { ThemeProvider } from '@fluentui/react';
import { theme } from 'assets/theme';
import { BrowserRouter } from 'react-router-dom';
import { initializeIcons } from '@fluentui/react/lib/Icons';
import { WorldContextWrapper } from 'contexts/WorldContext';
const provider = (props: PropsWithChildren<{}>) => {
  initializeIcons();
  return (
    <ThemeProvider theme={theme} style={{ width: '100%', height: '100%' }}>
      <WorldContextWrapper>
        <BrowserRouter>{props.children}</BrowserRouter>
      </WorldContextWrapper>
    </ThemeProvider>
  );
};

export default provider;
