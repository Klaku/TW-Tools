import React, { PropsWithChildren, useContext, useState } from 'react'
import styled from 'styled-components'
import * as Panel from 'components/templates/Panel/PanelWrapper.styles'
import { ComboBox, DefaultButton, IComboBox, IComboBoxOption, PrimaryButton, ProgressIndicator, TextField, ZIndexes } from '@fluentui/react'
import { Village } from 'types/Village'
import { VillageContext } from 'contexts/VillageContext'
import { PlayerContext } from 'contexts/PlayerContext'
import { TribeContext } from 'contexts/TribeContext'
import { Tribe } from 'types/Tribe'
import PlenerTribeSelector from 'components/organisms/PlenerTribeSelector/PlenerTribeSelector'
const Plener = (props: PropsWithChildren<{}>) => {
    let context = {
        Village: useContext(VillageContext),
        Tribe: useContext(TribeContext),
        Player: useContext(PlayerContext),
    }

    const [level, setLevel] = useState(0)

    return (
        <Panel.Wrapper>
            <Panel.Title>Planer Ataków</Panel.Title>
            <PlenerTribeSelector />
        </Panel.Wrapper>
    )
}

export const VerticalContainer = styled.div`
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 15px;
`

export const HorizontalContainer = styled.div`
    display: flex;
    flex-direction: row;
    width: 100%;
    align-items: flex-start;
    gap: 15px;
`

export default Plener
