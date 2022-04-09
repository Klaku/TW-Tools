import React, { PropsWithChildren, useContext, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { TribeContext } from 'contexts/TribeContext';
import { Item, ListWrapper, Col, HeaderRow, DataContainer, DataRow, DataItem, ExpandContainer } from './PlayerRanking.styles';
import ValueColor from 'components/atoms/ValueColor';
import { Icon } from '@fluentui/react';
import { Tribe } from 'types/Tribe';
import { useNavigate, useParams } from 'react-router-dom';
import { WorldContext } from 'contexts/WorldContext';
import { PlayerContext } from 'contexts/PlayerContext';
import { Player } from 'types/Player';

const PlayerRanking = (props: PropsWithChildren<{ filter: string }>) => {
  const Contexts = {
    Player: useContext(PlayerContext),
    World: useContext(WorldContext),
  };
  const { sort, method } = useParams();
  const navigate = useNavigate();
  const redirect = (key: string) => {
    if (sort == key && method != 'desc') {
      navigate(`/${Contexts.World.selected?.SubDomain}/rank/player/${key}/desc`);
    } else {
      navigate(`/${Contexts.World.selected?.SubDomain}/rank/player/${key}/asc`);
    }
  };
  const keySort = (a: Player, b: Player) => {
    let c, d;
    switch (sort) {
      case 'nr':
      case 'points':
        c = a.Points;
        d = b.Points;
        break;
      case 'name':
        c = a.Name;
        d = b.Name;
        break;
      case 'villages':
        c = a.VillagesCount;
        d = b.VillagesCount;
        break;
      case 'ra':
        c = a.RA;
        d = b.RA;
        break;
      case 'ro':
        c = a.RO;
        d = b.RO;
        break;
      case 'rs':
        c = a.RS;
        d = b.RS;
        break;
      default:
        d = a.Ranking;
        c = b.Ranking;
        break;
    }
    return (c < d ? -1 : 1) * (method == 'desc' ? 1 : -1);
  };
  const VisiblePlayers = Contexts.Player.players.filter((x) => x.Name.toLowerCase().indexOf(props.filter.toLowerCase()) != -1);
  return (
    <Panel.Section>
      <Panel.Title2>Ranking graczy</Panel.Title2>
      <ListWrapper>
        <HeaderRow>
          <Col
            onClick={() => {
              redirect('nr');
            }}
            style={{ width: 25, cursor: 'pointer' }}>
            #
          </Col>
          <Col style={{ width: 50 }}>Plemie</Col>
          <Col
            onClick={() => {
              redirect('name');
            }}
            style={{ minWidth: 50, flexGrow: 1, cursor: 'pointer' }}>
            Nazwa
          </Col>
          <Col
            onClick={() => {
              redirect('points');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            Punkty
          </Col>
          <Col
            onClick={() => {
              redirect('villages');
            }}
            style={{ width: 60, cursor: 'pointer' }}>
            Wioski
          </Col>
          <Col
            onClick={() => {
              redirect('ra');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RA
          </Col>
          <Col
            onClick={() => {
              redirect('ro');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RO
          </Col>
          <Col
            onClick={() => {
              redirect('rs');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RS
          </Col>
          <ExpandContainer></ExpandContainer>
        </HeaderRow>
        {VisiblePlayers.sort(keySort).map((player) => {
          return <ListItemComponent key={player.Id} player={player} />;
        })}
      </ListWrapper>
    </Panel.Section>
  );
};

const ListItemComponent = (props: PropsWithChildren<{ player: Player }>) => {
  const { player } = props;
  const [isCollapsed, setIsCollapsed] = useState(true);
  const Tribe = useContext(TribeContext);
  const RowData = (title: string, val: number, val24: number, val7: number, val30: number) => {
    return (
      <DataRow>
        <DataItem style={{ minWidth: 150 }}>{title}</DataItem>
        <DataItem>{Number(val).toLocaleString('de')}</DataItem>
        <DataItem>
          {val24 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val24)}>
              {Math.abs(Number(val - val24)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <DataItem>
          {val7 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val7)}>
              {Math.abs(Number(val - val7)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <DataItem>
          {val30 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val30)}>
              {Math.abs(Number(val - val30)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <ExpandContainer></ExpandContainer>
      </DataRow>
    );
  };
  return (
    <Item>
      <HeaderRow>
        <Col style={{ width: 25 }}>{player.Ranking}</Col>
        <Col style={{ minWidth: 50 }}>{Tribe.tribes.filter((x) => x.TribeId == player.TribeId)[0]?.Tag}</Col>
        <Col style={{ minWidth: 50, flexGrow: 1 }}>{player.Name}</Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.Points - player.Points24}>{Number(player.Points).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 60 }}>
          <ValueColor value={player.VillagesCount - player.VillagesCount24}>{Number(player.VillagesCount).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.RA - player.RA24}>{Number(player.RA).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.RO - player.RO24}>{Number(player.RO).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.RS - player.RS24}>{Number(player.RS).toLocaleString('de')}</ValueColor>
        </Col>
        {isCollapsed ? (
          <ExpandContainer
            style={{ cursor: 'pointer' }}
            onClick={() => {
              setIsCollapsed(false);
            }}>
            <Icon iconName={'ChevronDown'} />
          </ExpandContainer>
        ) : (
          <ExpandContainer
            style={{ cursor: 'pointer' }}
            onClick={() => {
              setIsCollapsed(true);
            }}>
            <Icon iconName={'ChevronUp'} />
          </ExpandContainer>
        )}
      </HeaderRow>
      {!isCollapsed && (
        <DataContainer>
          <DataRow style={{ backgroundColor: '#ddd' }}>
            <DataItem style={{ minWidth: 150 }}>Właściwość</DataItem>
            <DataItem>Aktualnie</DataItem>
            <DataItem>24 godziny</DataItem>
            <DataItem>7 dni</DataItem>
            <DataItem>30dni</DataItem>
            <ExpandContainer></ExpandContainer>
          </DataRow>
          {RowData('Ranking', player.Ranking, player.Ranking24, player.Ranking7, player.Ranking30)}
          {RowData('Liczba Punktów', player.Points, player.Points24, player.Points7, player.Points30)}
          {RowData('Liczba Wiosek', player.VillagesCount, player.VillagesCount24, player.VillagesCount7, player.VillagesCount30)}
          {RowData('Pokonani w Ataku', player.RA, player.RA24, player.RA7, player.RA30)}
          {RowData('Pokonani w Obronie', player.RO, player.RO24, player.RO7, player.RO30)}
          {RowData('Pokonani Razem', player.RS, player.RS24, player.RS7, player.RS30)}
        </DataContainer>
      )}
    </Item>
  );
};

export default PlayerRanking;
