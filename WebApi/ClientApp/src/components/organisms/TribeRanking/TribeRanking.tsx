import React, { PropsWithChildren, useContext, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { TribeContext } from 'contexts/TribeContext';
import { Item, ListWrapper, Col, HeaderRow, DataContainer, DataRow, DataItem, ExpandContainer } from './TribeRanking.styles';
import ValueColor from 'components/atoms/ValueColor';
import { Icon } from '@fluentui/react';
import { Tribe } from 'types/Tribe';
import { useNavigate, useParams } from 'react-router-dom';
import { WorldContext } from 'contexts/WorldContext';

const TribeRanking = (props: PropsWithChildren<{ filter: string }>) => {
  const Contexts = {
    Tribe: useContext(TribeContext),
    World: useContext(WorldContext),
  };
  const { sort, method } = useParams();
  const navigate = useNavigate();
  const redirect = (key: string) => {
    if (sort == key && method != 'desc') {
      navigate(`/${Contexts.World.selected?.SubDomain}/rank/tribe/${key}/desc`);
    } else {
      navigate(`/${Contexts.World.selected?.SubDomain}/rank/tribe/${key}/asc`);
    }
  };
  const keySort = (a: Tribe, b: Tribe) => {
    let c, d;
    switch (sort) {
      case 'nr':
      case 'points':
        c = a.Points;
        d = b.Points;
        break;
      case 'tag':
        c = a.Tag;
        d = b.Tag;
        break;
      case 'name':
        c = a.Name;
        d = b.Name;
        break;
      case 'villages':
        c = a.Villages;
        d = b.Villages;
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
  const VisibleTribes = Contexts.Tribe.tribes.filter(
    (x) => x.Name.toLocaleLowerCase().indexOf(props.filter.toLowerCase()) != -1 || x.Tag.toLocaleLowerCase().indexOf(props.filter.toLowerCase()) != -1
  );
  return (
    <Panel.Section>
      <Panel.Title2>Ranking plemion</Panel.Title2>
      <ListWrapper>
        <HeaderRow>
          <Col
            onClick={() => {
              redirect('nr');
            }}
            style={{ width: 25, cursor: 'pointer' }}>
            #
          </Col>
          <Col
            onClick={() => {
              redirect('tag');
            }}
            style={{ width: 60, cursor: 'pointer' }}>
            Tag
          </Col>
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
        {VisibleTribes.sort(keySort).map((tribe) => {
          return <ListItemComponent key={tribe.Id} tribe={tribe} />;
        })}
        {props.filter.length > 1 && (
          <ListItemComponent
            tribe={
              {
                Id: 0,
                WorldId: 0,
                TribeId: VisibleTribes[0].TribeId,
                Name: 'Sumarycznie',
                Tag: '',
                RA: VisibleTribes.map((x) => x.RA).reduce((a, b) => a + b),
                Villages: VisibleTribes.map((x) => x.Villages).reduce((a, b) => a + b),
                Villages7: VisibleTribes.map((x) => x.Villages7).reduce((a, b) => a + b),
                Villages24: VisibleTribes.map((x) => x.Villages24).reduce((a, b) => a + b),
                Villages30: VisibleTribes.map((x) => x.Villages30).reduce((a, b) => a + b),
                Points: VisibleTribes.map((x) => x.Points).reduce((a, b) => a + b),
                Points7: VisibleTribes.map((x) => x.Points7).reduce((a, b) => a + b),
                Points24: VisibleTribes.map((x) => x.Points24).reduce((a, b) => a + b),
                Points30: VisibleTribes.map((x) => x.Points30).reduce((a, b) => a + b),
                RA7: VisibleTribes.map((x) => x.RA7).reduce((a, b) => a + b),
                RA24: VisibleTribes.map((x) => x.RA24).reduce((a, b) => a + b),
                RA30: VisibleTribes.map((x) => x.RA30).reduce((a, b) => a + b),
                RO: VisibleTribes.map((x) => x.RO).reduce((a, b) => a + b),
                RO7: VisibleTribes.map((x) => x.RO7).reduce((a, b) => a + b),
                RO24: VisibleTribes.map((x) => x.RO24).reduce((a, b) => a + b),
                RO30: VisibleTribes.map((x) => x.RO30).reduce((a, b) => a + b),
                RS: VisibleTribes.map((x) => x.RS).reduce((a, b) => a + b),
                RS7: VisibleTribes.map((x) => x.RS7).reduce((a, b) => a + b),
                RS24: VisibleTribes.map((x) => x.RS24).reduce((a, b) => a + b),
                RS30: VisibleTribes.map((x) => x.RS30).reduce((a, b) => a + b),
                Ranking: VisibleTribes.map((x) => x.Ranking).reduce((a, b) => a + b),
                Ranking7: VisibleTribes.map((x) => x.Ranking7).reduce((a, b) => a + b),
                Ranking24: VisibleTribes.map((x) => x.Ranking24).reduce((a, b) => a + b),
                Ranking30: VisibleTribes.map((x) => x.Ranking30).reduce((a, b) => a + b),
              } as Tribe
            }
          />
        )}
      </ListWrapper>
    </Panel.Section>
  );
};

const ListItemComponent = (props: PropsWithChildren<{ tribe: Tribe }>) => {
  const { tribe } = props;
  const [isCollapsed, setIsCollapsed] = useState(true);
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
        <Col style={{ width: 25 }}>{tribe.Ranking}</Col>
        <Col style={{ width: 60 }}>{tribe.Tag}</Col>
        <Col style={{ minWidth: 50, flexGrow: 1 }}>{tribe.Name}</Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.Points - tribe.Points24}>{Number(tribe.Points).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 60 }}>
          <ValueColor value={tribe.Villages - tribe.Villages24}>{Number(tribe.Villages).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.RA - tribe.RA24}>{Number(tribe.RA).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.RO - tribe.RO24}>{Number(tribe.RO).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.RS - tribe.RS24}>{Number(tribe.RS).toLocaleString('de')}</ValueColor>
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
          {tribe.Id != 0 && RowData('Ranking', tribe.Ranking, tribe.Ranking24, tribe.Ranking7, tribe.Ranking30)}
          {RowData('Liczba Punktów', tribe.Points, tribe.Points24, tribe.Points7, tribe.Points30)}
          {RowData('Liczba Wiosek', tribe.Villages, tribe.Villages24, tribe.Villages7, tribe.Villages30)}
          {RowData('Pokonani w Ataku', tribe.RA, tribe.RA24, tribe.RA7, tribe.RA30)}
          {RowData('Pokonani w Obronie', tribe.RO, tribe.RO24, tribe.RO7, tribe.RO30)}
          {RowData('Pokonani Razem', tribe.RS, tribe.RS24, tribe.RS7, tribe.RS30)}
        </DataContainer>
      )}
    </Item>
  );
};

export default TribeRanking;
