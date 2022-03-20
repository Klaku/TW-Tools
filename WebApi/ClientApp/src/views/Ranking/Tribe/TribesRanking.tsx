import React, { PropsWithChildren, useContext, useEffect, useState } from 'react';
import * as Panel from 'components/templates/Panel/Panel.styles';
import { WorldContext } from 'contexts/WorldContext';
import { TribeCurrent } from 'types/Tribe';
import { ComboBox, DetailsList, DirectionalHint, IColumn, IComboBox, IComboBoxOption, SelectionMode, ShimmeredDetailsList, TextField, TooltipDelay, TooltipHost } from '@fluentui/react';
import * as Styled from './TribeRanking.styles';
import ValueColor from 'components/atoms/ValueColor';
import RankingItemDetails from 'components/molecules/RankingItemDetails';

const TribesRanking = (props: PropsWithChildren<{}>) => {
  const [tribes, setTribes] = useState([] as TribeCurrent[]);
  const [fetching, setFetching] = useState(true);
  const context = useContext(WorldContext);
  const [pageIndex, setPageIndex] = useState(0);
  const [pageSize, setPageSize] = useState(20);
  useEffect(() => {
    setFetching(true);
    if (context.selected != null) {
      fetch(`/api/Tribe/ListTribeData?worldId=${context.selected?.id}&skip=${pageIndex * pageSize}&top=${pageSize}`)
        .then((response) => response.json())
        .then((data: TribeCurrent[]) => {
          setTribes(data);
          setFetching(false);
        });
    }
  }, [context.selected, pageIndex, pageSize]);
  const _columns: IColumn[] = [
    {
      key: 'index',
      name: 'Ranking',
      fieldName: 'ranking',
      minWidth: 54,
      maxWidth: 54,
    },
    {
      key: 'name',
      name: 'Nazwa',
      fieldName: 'name',
      minWidth: 100,
    },
    {
      key: 'tag',
      name: 'Tag',
      fieldName: 'tag',
      minWidth: 100,
    },
    {
      key: 'points',
      name: 'Punkty',
      fieldName: 'points',
      minWidth: 100,
      onRender: (item: TribeCurrent) => {
        return (
          <TooltipHost
            directionalHint={DirectionalHint.leftCenter}
            delay={TooltipDelay.zero}
            content={<RankingItemDetails value={item.points} value24={item.points24} value7={item.points7} value30={item.points30} />}>
            <div>{Number(item.points).toLocaleString('de')}</div>
          </TooltipHost>
        );
      },
    },
    {
      key: 'villages',
      name: 'Wioski',
      fieldName: 'villages',
      minWidth: 100,
      onRender: (item: TribeCurrent) => {
        return (
          <TooltipHost
            directionalHint={DirectionalHint.leftCenter}
            delay={TooltipDelay.zero}
            content={<RankingItemDetails value={item.villages} value24={item.villages24} value7={item.villages7} value30={item.villages30} />}>
            <div>{Number(item.villages).toLocaleString('de')}</div>
          </TooltipHost>
        );
      },
    },
    {
      key: 'RA',
      name: 'RA',
      fieldName: 'ra',
      minWidth: 100,
      onRender: (item: TribeCurrent) => {
        return (
          <TooltipHost
            directionalHint={DirectionalHint.leftCenter}
            delay={TooltipDelay.zero}
            content={<RankingItemDetails value={item.ra} value24={item.ranking24} value7={item.ranking7} value30={item.ranking30} />}>
            <div>{Number(item.ra).toLocaleString('de')}</div>
          </TooltipHost>
        );
      },
    },
    {
      key: 'RO',
      name: 'RO',
      fieldName: 'ro',
      minWidth: 100,
      onRender: (item: TribeCurrent) => {
        return (
          <TooltipHost
            directionalHint={DirectionalHint.leftCenter}
            delay={TooltipDelay.zero}
            content={<RankingItemDetails value={item.ro} value24={item.rO24} value7={item.rO7} value30={item.rO30} />}>
            <div>{Number(item.ro).toLocaleString('de')}</div>
          </TooltipHost>
        );
      },
    },
    // {
    //   key: 'RW',
    //   name: 'RW',
    //   fieldName: 'rs',
    //   minWidth: 100,
    //   onRender: (item: TribeCurrent) => {
    //     return (
    //       <TooltipHost
    //         directionalHint={DirectionalHint.leftCenter}
    //         delay={TooltipDelay.zero}
    //         content={
    //           <RankingItemDetails
    //             value={item.rs - (item.ra + item.ro)}
    //             value24={item.rS24 - (item.rA24 + item.rO24)}
    //             value7={item.rS7 - (item.rA7 + item.rO7)}
    //             value30={item.rS30 - (item.rA30 + item.rO30)}
    //           />
    //         }>
    //         <div>{Number(item.rs - (item.ra + item.ro)).toLocaleString('de')}</div>
    //       </TooltipHost>
    //     );
    //   },
    // },
    {
      key: 'RS',
      name: 'RS',
      fieldName: 'rs',
      minWidth: 100,
      onRender: (item: TribeCurrent) => {
        return (
          <TooltipHost
            directionalHint={DirectionalHint.leftCenter}
            delay={TooltipDelay.zero}
            content={<RankingItemDetails value={item.rs} value24={item.rS24} value7={item.rS7} value30={item.rS30} />}>
            <div>{Number(item.rs).toLocaleString('de')}</div>
          </TooltipHost>
        );
      },
    },
  ];
  return (
    <Panel.Wrapper>
      <Panel.Title2>Plemiona</Panel.Title2>
      <Panel.Section>
        <ShimmeredDetailsList enableShimmer={fetching} items={tribes} columns={_columns} selectionMode={SelectionMode.none} />
      </Panel.Section>
      <Panel.Section>
        <Styled.FooterContainer>
          <div></div>
          <Styled.PaginationContainer>
            <Styled.PaginationNavItem
              onClick={() => {
                setPageIndex(Math.max(0, pageIndex - 1));
              }}>
              Wstecz
            </Styled.PaginationNavItem>
            <Styled.PaginationNavItem>{pageIndex + 1}</Styled.PaginationNavItem>
            <Styled.PaginationNavItem
              onClick={() => {
                setPageIndex(pageIndex + 1);
              }}>
              Dalej
            </Styled.PaginationNavItem>
          </Styled.PaginationContainer>
          <Styled.PaginationSizeContainer>
            <ComboBox
              useComboBoxAsMenuWidth
              defaultSelectedKey={pageSize.toString()}
              onChange={(event: React.FormEvent<IComboBox>, option?: IComboBoxOption, index?: number, value?: string) => {
                setPageSize(Number(option?.key));
              }}
              options={[20, 50, 100].map((x) => {
                return {
                  key: x.toString(),
                  text: x.toString(),
                };
              })}
            />
          </Styled.PaginationSizeContainer>
        </Styled.FooterContainer>
      </Panel.Section>
    </Panel.Wrapper>
  );
};

export default TribesRanking;
