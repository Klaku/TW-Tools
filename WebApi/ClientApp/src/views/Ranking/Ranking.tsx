import React, { PropsWithChildren, useContext, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { DefaultButton, TextField } from '@fluentui/react';
import { WorldContext } from 'contexts/WorldContext';
import TribeRanking from 'components/organisms/TribeRanking/TribeRanking';
import PlayerRanking from 'components/organisms/PlayerRanking/PlayerRanking';
const Ranking = (props: PropsWithChildren<{}>) => {
  const { type, sort, method } = useParams();
  const [filter, setFilter] = useState('');
  const navigate = useNavigate();
  const Contexts = {
    World: useContext(WorldContext),
  };
  return (
    <Panel.Wrapper>
      <Panel.Title>Ranking</Panel.Title>
      <Panel.Section>
        <Panel.SectionRow>
          <DefaultButton
            text="Plemiona"
            onClick={() => {
              navigate(`/${Contexts.World.selected?.subDomain}/rank/tribe${sort ? `/${sort}${method ? `/${method}` : ''}` : ''}`);
            }}
          />
          <DefaultButton
            text="Gracze"
            onClick={() => {
              navigate(`/${Contexts.World.selected?.subDomain}/rank/player${sort ? `/${sort}${method ? `/${method}` : ''}` : ''}`);
            }}
          />
          <TextField
            value={filter}
            placeholder={'Filter by tag or name'}
            onChange={(e) => {
              setFilter((e.target as HTMLInputElement).value);
            }}
          />
        </Panel.SectionRow>
      </Panel.Section>
      {type == 'tribe' && <TribeRanking filter={filter} />}
      {type == 'player' && <PlayerRanking filter={filter} />}
    </Panel.Wrapper>
  );
};

export default Ranking;
