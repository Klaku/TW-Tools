import React, { PropsWithChildren } from 'react';
import { ThemeProvider } from '@fluentui/react';
import { theme } from 'assets/theme';
import { BrowserRouter } from 'react-router-dom';
import { initializeIcons } from '@fluentui/react/lib/Icons';
import { WorldContextWrapper } from 'contexts/WorldContext';
import { TribeContextWrapper } from 'contexts/TribeContext';
import { PlayerContextWrapper } from 'contexts/PlayerContext';
import { VillageContextWrapper } from 'contexts/VillageContext';
const provider = (props: PropsWithChildren<{}>) => {
  initializeIcons();
  return (
    <ThemeProvider theme={theme} style={{ width: '100%', height: '100%' }}>
      <WorldContextWrapper>
        <TribeContextWrapper>
          <PlayerContextWrapper>
            <VillageContextWrapper>
              <BrowserRouter>{props.children}</BrowserRouter>
            </VillageContextWrapper>
          </PlayerContextWrapper>
        </TribeContextWrapper>
      </WorldContextWrapper>
    </ThemeProvider>
  );
};

export default provider;
