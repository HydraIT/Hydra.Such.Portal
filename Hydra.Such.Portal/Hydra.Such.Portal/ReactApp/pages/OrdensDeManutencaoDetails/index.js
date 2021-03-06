// https://github.com/diegohaz/arc/wiki/Atomic-Design
/*react-styleguide: ignore*/
import React, { Component } from 'react';
import axios from 'axios';
import { PageTemplate } from 'components';
import CircularProgress from '@material-ui/core/CircularProgress';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import styled, { css, theme, injectGlobal, withTheme } from 'styled-components';
import MuiGrid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import _theme from '../../themes/default';
import { Button, Text, Icon, Circle, Wrapper, OmDatePicker, CheckBox, Input, Avatars, Modal, Tooltip, Spacer, Breadcrumb } from 'components';
import MuiDeleteIcon from '@material-ui/icons/Delete';
import DragIndicatorIcon from '@material-ui/icons/DragIndicator';
import moment from 'moment';
import ReactDOM from 'react-dom';
import Hidden from '@material-ui/core/Hidden';
import { createMuiTheme } from '@material-ui/core/styles';
import MuiTableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import MuiAddIcon from '@material-ui/icons/Add';
import AutoSizer from "react-virtualized-auto-sizer";
import { FixedSizeList as ListWindow } from "react-window";
import Color from 'color';
import Highlighter from "react-highlight-words";
import MuiTextField from '@material-ui/core/TextField';
import MuiInput from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';
import { renderToString } from 'react-dom/server';
import { string } from 'prop-types';
import { withRouter } from 'react-router-dom';
import {
        Column,
        FilteringState, GroupingState,
        IntegratedFiltering, IntegratedGrouping, IntegratedPaging, IntegratedSelection, IntegratedSorting,
        PagingState, SelectionState, SortingState, DataTypeProvider, DataTypeProviderProps, CustomGrouping,
        TreeDataState, CustomTreeData, RowDetailState, VirtualTableState, SearchState
} from '@devexpress/dx-react-grid';

import {
        DragDropProvider,
        Grid as TGrid, PagingPanel,
        Table, TableFilterRow, TableGroupRow,
        TableHeaderRow, TableSelection, Toolbar, GroupingPanel, VirtualTable,
        TableColumnReordering, ColumnChooser, TableColumnVisibility, TableColumnResizing,
        SearchPanel, VirtualTableView
} from '@devexpress/dx-react-grid-material-ui';
import { isAbsolute } from 'upath';
import { hidden } from 'ansi-colors';
import { throws } from 'assert';

axios.defaults.headers.post['Accept'] = 'application/json';
axios.defaults.headers.get['Accept'] = 'application/json';

const { DialogTitle, DialogContent, DialogActions } = Modal;

const muiTheme = createMuiTheme();
const breakpoints = muiTheme.breakpoints.values;

injectGlobal`
    body {
        background-color: white;   
    }
    .navbar-container, .navbar-header {
        background-color: ${_theme.palette.secondary.default};
    }
    .app-main {
        .row {
            margin: 0;
        }
        .wrap {
            padding: 0;
        }
    }
    @keyframes fade {
        from { opacity: 1.0; }
        50% { opacity: 0.5; }
        to { opacity: 1.0; }
    }                                                                                                                                                                                                                                  

    @-webkit-keyframes fade {
        from { opacity: 1.0; }
        50% { opacity: 0.5; }
        to { opacity: 1.0; }
    }
    .blink {
        animation:fade 1000ms infinite;
        -webkit-animation:fade 1000ms infinite;
    }
    mark, .mark {
        background-color: ${_theme.palette.search} ;
        padding: 0;
    }
        [class*="MuiTableRow"] {
                .first-cell {
                        [class*="icon"] {
                                left: 24px !important;
                        }
                }
        }
        [class*="GroupPanelContainer"] {
                [class*="MuiChip-root"] {
                        position: relative;
                        background: ${_theme.palette.primary.default};
                        border-radius: 7px;
                        padding: 0;
                        color: white;
                        font-family: Inter,Helvetica,sans-serif;
                        font-style: normal;
                        font-weight: 400;
                        font-size: 12px;
                        line-height: 16px;
                        text-transform: uppercase;
                        margin: 0 5px;
                        height: 24px;
                        &:hover, &:active, &:focus {
                                background: ${_theme.palette.primary.default};
                                border-radius: 7px;
                                color: white;
                                font-family: Inter,Helvetica,sans-serif;
                                font-style: normal;
                                font-weight: 400;
                                font-size: 12px;
                                line-height: 16px;
                                text-transform: uppercase;
                                margin: 0 5px;
                        }
                        [class*="MuiButtonBase-root"] {
                                color: white;
                        }
                        [class*="MuiChip-label"] {
                                padding-left: 8px;
                                padding-right: 0px;
                        }
                        [class*="MuiChip-deleteIcon"] {
                                color: ${_theme.palette.primary.default};
                        }
                        &[class*="MuiChip-deletable"] {
                                &:before {
                                        font-family: 'eSuch' !important;
                                        content: '\\e90b';
                                        color: white;
                                        position: absolute;
                                        top: 4px;                                                                
                                        right: 4px;
                                        pointer-events: none;
                                }
                        }
                }
        }
        [class*="MuiPopover-paper"] {
                [class*="DragDrop-container"] {
                        [role="button"] {
                                background: ${_theme.palette.primary.default};
                                border-radius: 7px;
                                padding: 0;
                                color: white;
                                font-family: Inter,Helvetica,sans-serif;
                                font-style: normal;
                                font-weight: 400;
                                font-size: 12px;
                                line-height: 16px;
                                text-transform: uppercase;
                                margin: 0 5px;
                                height: 24px;
                                opacity: .3;
                        }
                }
                [class*="MuiSvgIcon-root"] {
                        &[focusable] {
                                color: ${_theme.palette.primary.default}
                        }
                }
                [class*="MuiTypography-root"] {
                        font-family: Inter,Helvetica,sans-serif;
                        font-style: normal;
                        font-weight: 400;
                        font-size: 14px;
                        line-height: 24px;
                        margin: 0;
                        color: #323F4B;
                }
        }
        .table--row--hoverable {
                cursor: pointer;
                &:hover {
                        background: ${_theme.palette.bg.grey};
                }
        }
        
`

const Grid = styled(MuiGrid)`
    position: relative;

`

const TableCell = styled(MuiTableCell)` && { 
        padding: 15px 24px 15px 24px;
        font-size: inherit;
        color: ${_theme.palette.primary.default};
        white-space: nowrap;
        text-overflow: ellipsis;
        border: none;
        max-width: 12vw;
        overflow: hidden;
        p {
            margin: 0;
        }
    }
`;

const fetchEquipment = ({ MoNo, ObjectNo, Nome, Sala, NumSerie, NumInventario, IdEquipEstado }, cb) => {
        cb = cb || function () { };
        axios.get('/ordens-de-manutencao/equipment', {
                params: { MoNo, ObjectNo, Nome, Sala, NumSerie, NumInventario, IdEquipEstado }
        })
                .then(function (data) {
                        cb(null, data);
                }).catch(function (error) {
                        cb(error);
                });
}

const PickerButton = styled(Button)` && {
        position: relative;
        z-index: 10;
        padding-left: 25px;
        padding-right: 25px;
    }
`;

const CircleOm = {
        wrapper: styled.div`
        margin: auto;
        white-space: nowrap;
        ${props => props.primary && css`
        
        `}
    `,
        icon: styled.div`
        font-size: 33px;
        line-height: 35px;
        display: inline-block;
        vertical-align: middle;
        padding: 17px;
        > [class*="icon"] {
            text-align: center;
            width: 57px;
            height: 57px;
            display: inline-block;
            background:  ${props => props.background};
            color:  ${props => props.color};
            border-radius: 50%;
            line-height: 56px;
            font-size: 70px;
            position: relative;
            &:before {
                position: absolute;
                left: -7px;
                right: -7px;
                top: -7px;
                bottom: -7px;
                margin: auto;
                text-align: center;
                line-height: 70px;
                font-size: ${props => props.fontSize ? props.fontSize : '33px'};
            }
        }
    `,
        chart: styled.div`
        padding: 6px;
        display: inline-block;
        vertical-align: middle;
        text-align: center;
    `,
};

const CircleOmWrapper = styled(CircleOm.wrapper)`
    @media (max-width: ${breakpoints["sm"] + "px"}) {
        transform: scale(0.8) translateX(-50%);
        transform-origin: 0;
        left: 50%;
        position: relative;
    }
    @media (max-width:  455px ) {
        transform: scale(0.7) translateX(-50%);
        margin-top: -20px;
        margin-bottom: -20px;
    }
    @media (max-width:  400px ) {
        transform: scale(0.6) translateX(-50%);
        margin-top: -30px;
        margin-bottom: -30px;
    }
    @media (max-width:  350px ) {
        transform: scale(0.5) translateX(-50%);
        margin-top: -40px;
        margin-bottom: -40px;
    }
`;

const PullRight = styled.span`
    float: right;
`

const TextHeader = styled(Text)`
    display: inline-block;
`

const TbleIcon = styled(Icon)`
    font-size: 24px;
`

const ListContainer = styled.div`
    position: absolute;
    top:0;
    left:0;
    right:0;
    bottom: 0;
    z-index: 0;
    overflow: auto;
    padding: 0;
    [class*="RootBase-root"]{
            position: absolute;
            top: 0;
            bottom: 0;
    }
`
const Hr = styled.hr`
    margin-bottom: 0;
    margin-top: 0;
`;
//updateTechnicals({ orderId: "OM1901562", technicalsId: ["42664", "105590", "106624"] });

const ListGridRow = styled(Grid)`  
    padding: 16px 0px;
    cursor: pointer;
    &:hover {
        background-color: ${Color(_theme.palette.primary.keylines).rgb().fade(0.5).toString()};
    }
`

const ListGridItem = styled(Grid)`  
    padding: 10px 25px;
`

const ListGridItemTextOverflow = styled(ListGridItem)`
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    p {
        margin:0;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
`

const TextDiv = styled(Text)``.withComponent('div');

const TextField = styled(MuiInput)` && {
        margin: 0;
        display: block;
        margin: 0 auto;
        display: block;
        &[class*="MuiInputBase-focused"] {
            width: 100%;
            button {
                right: 7px;
                color: ${_theme.palette.secondary.default};
                pointer-events: none;
            }
            &:before {
                border-bottom: 1px solid ${_theme.palette.primary.dark} !important;
            }
            &:after {
                border-bottom: 1px solid ${_theme.palette.primary.dark} !important;
            }
        }
        &:before {
                border-bottom: 0px solid ${_theme.palette.primary.dark} !important;
        }
        &:after {
                border-bottom: 1px solid ${_theme.palette.primary.dark} !important;
        }
        &:hover {
            &:before  {
                border-bottom: 0px solid ${_theme.palette.primary.dark} !important;
            }
        }
       
        input {
            font-family: ${_theme.fonts.primary};
            font-style: normal;
            font-weight: 400;
            font-size: 14px;
            line-height: 24px;
            height: auto;
            padding-left: 10px;
            padding-right: ${props => props.affix ? '40px' : '5px'};
            padding-top: 10px;
            padding-bottom: 10px;
            width: 100%;
            color: transparent;
            text-shadow: 0 0 0 ${props => Color(props.color || _theme.palette.primary.dark).opaquer(-0.1).rgb().toString()};
            letter-spacing: .02em;
            &:focus {
                transition: all 200ms cubic-bezier(0.4, 0, 0.2, 1) .3s;
                color: ${props => props.color || _theme.palette.primary.dark};
                text-shadow: none;
            }
        }
    }
`;

const SearchButton = styled(Button)` && {
        transition: right 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
        background: none;
        position: absolute;
        right: 25px;
        top: 0px;
        color: ${_theme.palette.primary.default};
        [class*="icon"] {
            font-size: 27px;
        }
    }
`;

const SearchWrapper = styled.div`
    position: absolute;
    display: block;
    width: ${props => props.width || '50%'};
    bottom: 0;
    right: 0;
    z-index: 2;
    padding: 0 25px 0;
`;

var timer = 0;

const BoldFormatter = ({ value }) => <Text b >{value}werwerwerwe</Text>;
const BoldTypeProvider = props => (
        <DataTypeProvider
                formatterComponent={BoldFormatter}
                {...props}
        />
);
const getRowId = row => row.idEquipamento || 0;

class OrdensDeManutencaoLine extends Component {
        state = {
                orderId: "",
                isLoading: false,
                ordersCountsLines: {
                        toSigning: null,
                        toExecute: null,
                        executed: null
                },
                marcas: [],
                servicos: [],
                group: [{ columnName: 'categoriaText' }],
                hiddenColumns: [],
                defaultExpandedGroups: [],
                groupingStateColumnExtensions: [
                        { columnName: 'estado', groupingEnabled: false },
                        { columnName: 'categoriaText', groupingEnabled: false },
                        { columnName: 'nome', groupingEnabled: false },
                        { columnName: 'numSerie', groupingEnabled: false },
                        { columnName: 'numInventario', groupingEnabled: false },
                        { columnName: 'numEquipamento', groupingEnabled: false },
                        { columnName: 'action', groupingEnabled: false }
                ],
                tooltipReady: false,
                maintenanceOrder: {},
                maintenanceOrdersLines: [],
                maintenanceOrdersTotal: 0,
                maintenanceOrdersLinesFiltered: [],
                maintenanceOrdersLinesIsLoading: true,
                skip: 0,
                searchValue: "",
                isSearching: false,
                maintenanceOrdersLinesNext: "",
                listContainerStyle: {},
                avatarColors: [
                        "#990000",
                        "#33DDEE",
                        "#5533DD",
                        "#339900",
                        "#cc00cc"
                ]
        }

        constructor(props) {
                super(props);
                moment.locale("pt");
                this.handleResize = this.handleResize.bind(this);
                this.getInitials = this.getInitials.bind(this);
                this.handleGridScroll = this.handleGridScroll.bind(this);
                this.filterListByKeysValue = this.filterListByKeysValue.bind(this);
                this.state.orderId = this.props.match.params.orderid;
                this.fetchmaintenanceOrdersLines({ orderId: this.state.orderId }, () => {
                        this.setState({
                                defaultExpandedGroups: getDefaultExpandedGroups(this.state.maintenanceOrdersLines, this.state.group),
                                // maintenanceOrdersLinesFiltered: this.filterListByKeysValue({
                                //         list: this.state.maintenanceOrdersLines, keys: ['Idequipamento', 'nome', 'categoriaText', 'numSerie', 'numInventario', 'numEquipamento', 'marcaText', 'servicoText', 'idRegiao'], value: this.state.search
                                // })
                        }, () => {
                        });
                });
        }

        fetchmaintenanceOrdersLines({ orderId, search }, cb) {
                this.setState({
                        isLoading: true
                });

                cb = cb || (() => { });
                var filter = null;
                if (search && search != '') {
                        filter = "contains(nome,'" + search + "') or contains(numInventario,'" + search + "') or contains(numEquipamento,'" + search + "') or contains(numSerie,'" + search + "')";
                        var _marcas = this.state.marcas.filter((item) => { return item.nome.toLowerCase().indexOf(this.state.searchValue.toLowerCase()) > -1; }).map(item => item.idMarca).join(',');
                        if (_marcas.length > 0) {
                                filter += " or marca in (" + _marcas + ")"
                        }
                }
                axios.get(`/ordens-de-manutencao/${orderId}`, {
                        params: {
                                $select: 'Idequipamento,nome,categoria,numSerie,numInventario,numEquipamento,marca,idServico,idRegiao',
                                $filter: filter,
                                $count: true
                        }
                }).then((resultLines) => {
                        var data = resultLines.data;
                        this.setTableMarginTop();
                        if (data.ordersCountsLines && data.resultLines && data.resultLines.items) {
                                var list = data.resultLines.items;
                                var nextPageLink = data.resultLines.nextPageLink;
                                // var params = nextPageLink.split('$');
                                // var skip = 0;
                                // if (params.length > 0) {
                                //         var skipSplit = params[params.length - 1].split('=');
                                //         skip = skipSplit[1];
                                //         skip = skip - 30;
                                // }


                                this.setState({
                                        maintenanceOrder: data.order,
                                        ordersCountsLines: data.ordersCountsLines,
                                        maintenanceOrdersLines: list,
                                        maintenanceOrdersTotal: data.resultLines.count,
                                        maintenanceOrdersLinesNext: nextPageLink,
                                        marcas: data.marcas,
                                        skip: 0,
                                }, () => {
                                        this.setState({
                                                defaultExpandedGroups: getDefaultExpandedGroups(this.state.maintenanceOrdersLines, this.state.group)
                                        }, () => {
                                                cb(null, data);
                                        });
                                });
                        }
                }).catch(function (error) {
                }).then(() => {
                        this.setState({ isLoading: false, maintenanceOrdersLinesIsLoading: false, isSearching: false });
                        setTimeout(() => {
                                this.setState({ tooltipReady: true });
                                Tooltip.Hidden.hide();
                                Tooltip.Hidden.rebuild();
                        }, 1200);
                });
        }

        fetchNext = (skip, take) => {
                // return;
                // console.log('TEWTTE', skip);
                // if (skip == 0) {
                //         console.log(this.state.maintenanceOrdersLines);
                //         this.setState({
                //                 maintenanceOrdersLinesTeste: this.state.maintenanceOrdersLines
                //         }, () => {
                //         });
                // } else {
                if (this.state.maintenanceOrdersLinesNext && !this.state.maintenanceOrdersLinesIsLoading && !this.state.isLoading && this.state.maintenanceOrdersTotal > this.state.maintenanceOrdersLines.length) {
                        this.setState({ maintenanceOrdersLinesIsLoading: true }, () => {
                                axios.get(this.state.maintenanceOrdersLinesNext).then((resultLines) => {
                                        var data = resultLines.data;
                                        if (data.ordersCountsLines && data.resultLines && data.resultLines.items) {
                                                var list = data.resultLines.items;
                                                var nextPageLink = data.resultLines.nextPageLink;
                                                var maintenanceOrdersLines;
                                                if (list.length > 0) {
                                                        maintenanceOrdersLines = this.state.maintenanceOrdersLines.concat(list);
                                                } else {
                                                        maintenanceOrdersLines = this.state.maintenanceOrdersLines;
                                                }
                                                // maintenanceOrdersLines = _.uniqBy(maintenanceOrdersLines, 'Idequipamento');
                                                this.setState({
                                                        ordersCountsLines: data.ordersCountsLines,
                                                        maintenanceOrdersLines: maintenanceOrdersLines,
                                                        maintenanceOrdersLinesNext: nextPageLink,
                                                        //maintenanceOrdersTotal: data.resultLines.count,
                                                        // skip: skip,
                                                }, () => {
                                                        //cb(null, data);
                                                        this.setState({
                                                                // maintenanceOrdersLinesFiltered: this.filterListByKeysValue({
                                                                //         list: this.state.maintenanceOrdersLines, keys: ['Idequipamento', 'nome', 'categoriaText', 'numSerie', 'numInventario', 'numEquipamento', 'marcaText', 'servicoText', 'idRegiao'], value: this.state.search
                                                                // }),
                                                                defaultExpandedGroups: getDefaultExpandedGroups(this.state.maintenanceOrdersLines, this.state.group),
                                                        }, () => {
                                                        });
                                                });
                                        }
                                }).catch().then(() => {
                                        this.setState({ maintenanceOrdersLinesIsLoading: false });
                                        setTimeout(() => {
                                                this.setState({ tooltipReady: true });
                                                Tooltip.Hidden.hide();
                                                Tooltip.Hidden.rebuild();
                                        }, 1200);
                                });;
                        });
                }
                // }
        }

        componentDidMount() {
                window.addEventListener("resize", this.handleResize);
                this.setDatePickerMarginTop();
                this.setTableMarginTop();
        }

        handleResize() {
                (() => {
                        setTimeout(() => {
                                this.setDatePickerMarginTop();
                                this.setTableMarginTop();
                        }, 0)
                })();
        }

        handleGridScroll(e) {
                setTimeout(() => {
                        Tooltip.Hidden.hide();
                        Tooltip.Hidden.rebuild();
                });
        }

        setTableMarginTop() {
                (() => {
                        setTimeout(() => {
                                if (typeof $ == 'undefined') {
                                        return setTimeout(() => {
                                                this.setTableMarginTop();
                                        }, 600);
                                }
                                var highlightWrapper = ReactDOM.findDOMNode(this.highlightWrapper);
                                var listContainer = ReactDOM.findDOMNode(this.listContainer);
                                var hr = 20;
                                var top = (/*(document.getElementById("app-navbar-collapse").offsetHeight * 1) +*/ (highlightWrapper.offsetHeight * 1)) + hr;
                                var appNavbarCollapse = document.getElementById("app-navbar-collapse");
                                if (appNavbarCollapse) {
                                        var height = window.innerHeight - top - (document.getElementById("app-navbar-collapse").offsetHeight * 1);
                                        this.setState({ listContainerStyle: { "height": height, marginTop: top } })
                                }
                        }, 0)
                })();
        }

        getInitials(name) {
                var initials = name.match(/\b\w/g) || [];
                initials = ((initials.shift() || '') + (initials.pop() || '')).toUpperCase();
                return initials;
        }

        setDatePickerMarginTop() {
                if (window.outerWidth < 960) {
                        var pickerButton = ReactDOM.findDOMNode(this.pickerButton);
                        var pickerButtonTop = (pickerButton.getBoundingClientRect().top * 1) - 70;
                        this.setState({ datePickerMarginTop: pickerButtonTop + 'px' });
                } else if (this.state.datePickerMarginTop != 0) {
                        this.setState({ datePickerMarginTop: 0 });
                }
        }

        filterListByKeysValue({ list, keys, value }) {
                keys = keys || [];
                value = value || "";
                value = value.toLowerCase();
                let filteredList = list.filter((item) => {
                        var find = false;
                        keys.map((k) => {
                                if (item[k] && item[k].toString().toLowerCase().search(value) != -1) {
                                        find = true;
                                }
                        });
                        return find;
                });
                return filteredList;
        }
        getChildRows = (row, rootRows) => {
                const childRows = rootRows.filter(r => r.parentId === (row ? row.id : null));
                return childRows.length ? childRows : null;
        };

        render() {
                const { isLoading, ordersCountsLines, maintenanceOrdersLines, maintenanceOrdersLinesIsLoading, isSearching } = this.state;

                var columns = [
                        { columnName: 'estado', name: 'estado', title: ' ', getCellValue: row => row.estado, sortingEnabled: false, selectionEnabled: false },
                        { columnName: 'categoriaText', name: 'categoriaText', title: 'Equipamentos', getCellValue: row => row.categoriaText },
                        { columnName: 'nome', name: 'nome', title: 'Nome', dataType: 'bold', getCellValue: row => row.nome },
                        { columnName: 'marcaText', name: 'marcaText', title: 'Marca', getCellValue: row => row.marcaText },
                        { columnName: 'servicoText', name: 'servicoText', title: 'Serviço', getCellValue: row => row.servicoText },
                        { columnName: 'numSerie', name: 'numSerie', title: 'Nº Série', getCellValue: row => row.numSerie },
                        { columnName: 'numInventario', name: 'numInventario', title: 'Nº Inventário', getCellValue: row => row.numInventario },
                        { columnName: 'numEquipamento', name: 'numEquipamento', title: 'Nº Equipamento', getCellValue: row => row.numEquipamento },
                        { columnName: 'action', name: 'action', title: ' ', sortingEnabled: false, selectionEnabled: false },
                ];
                var tableColumnExtensions = [
                        { columnName: 'estado', width: 60 },
                        { columnName: 'categoriaText' },
                        { columnName: 'nome' },
                        { columnName: 'marcaText' },
                        { columnName: 'servicoText' },
                        { columnName: 'numSerie' },
                        { columnName: 'numInventario' },
                        { columnName: 'numEquipamento' },
                        { columnName: 'action', width: 60 },
                ];

                var headColumns = _.differenceBy(columns, this.state.group, 'columnName');
                headColumns = headColumns.filter((val) => {
                        return this.state.hiddenColumns.indexOf(val.columnName) < 0;
                });
                var firstColumn = headColumns[0];

                return (
                        <PageTemplate >
                                <Wrapper padding={'0 0 20px'} width="100%">
                                        <Wrapper padding={' 20px 25px'} width="100%" >
                                                {!this.state.isLoading ? (<span>
                                                        <Text b onClick={() => { this.props.history.push(`/ordens-de-manutencao`) }} style={{ verticalAlign: 'middle', textDecoration: 'underline', cursor: 'pointer' }}>OMs</Text>
                                                        <Icon arrow-right style={{ verticalAlign: 'middle' }} />&nbsp;
                                                        <Text b style={{ verticalAlign: 'middle', position: 'relative', top: '1px' }} >{toTitleCase(this.state.maintenanceOrder.description)}</Text>
                                                </span>) : ''}
                                        </Wrapper>

                                        <Grid container direction="row" justify="space-between" alignitems="top" spacing={0} maxwidth={'100%'} margin={0} ref={el => this.highlightWrapper = el} padding={"200px"}>
                                                <Grid item xs>

                                                        {!this.state.isLoading ?
                                                                <Wrapper padding={'0 25px 0'}>
                                                                        <TextHeader h2><b> {toTitleCase(this.state.maintenanceOrder.nomeInstituicao)}</b></TextHeader> <br />
                                                                        {this.state.orderId ?
                                                                                <div><Text b>OM</Text> {this.state.orderId} </div>
                                                                                : ''}
                                                                        {this.state.maintenanceOrder.contrato ?
                                                                                <div><Text b>Contrato</Text> {this.state.maintenanceOrder.contrato} </div>
                                                                                : ''}
                                                                </Wrapper>
                                                                : ''}
                                                </Grid>
                                                <Grid container item md={6} xs={12}>
                                                        <CircleOmWrapper className={''}>
                                                                <CircleOm.icon background={_theme.palette.primary.dark} color={_theme.palette.primary.keylines} fontSize="71px" >
                                                                        <Spacer height="15px" />
                                                                        <Icon sad />
                                                                        <Wrapper textAlign="center">
                                                                                <Text dataSmall>300</Text>
                                                                        </Wrapper>
                                                                </CircleOm.icon>
                                                                <CircleOm.chart>
                                                                        <Circle loading={false} label="Equipamentos" strokeValue={241} trailValue={300} strokeIcon={<Icon happy />} trailIcon={<Icon sad />} width={191} />
                                                                </CircleOm.chart>
                                                                <CircleOm.icon background={'white'} color={_theme.palette.secondary.default} fontSize="71px" >
                                                                        <Spacer height="20px" />
                                                                        <Icon happy />
                                                                        <Wrapper textAlign="center">
                                                                                <Text dataSmall color={_theme.palette.secondary.default}>241</Text>
                                                                        </Wrapper>
                                                                </CircleOm.icon>
                                                        </CircleOmWrapper>
                                                </Grid>
                                                <Grid item xs>

                                                        <Hidden mdDown>
                                                                <Wrapper textAlign="center" inline>
                                                                        <Spacer height="25px" />
                                                                        <Text dataBig>
                                                                                20
                                                                        </Text>
                                                                        <Text p>
                                                                                Relatórios por assinar
                                                                        </Text>
                                                                </Wrapper>
                                                        </Hidden>

                                                </Grid>
                                        </Grid>
                                        <SearchWrapper width={"25%"}>
                                                <TextField
                                                        inputProps={{ autoComplete: "off" }}
                                                        id="oms-search"
                                                        onChange={(e) => {
                                                                let search = e.target.value.toLowerCase();
                                                                this.setState({
                                                                        searchValue: search, maintenanceOrdersLinesNext: "", maintenanceOrdersTotal: 0, skip: 0, maintenanceOrdersLines: [], defaultExpandedGroups: [],
                                                                        maintenanceOrdersLinesFiltered: this.filterListByKeysValue({ list: this.state.maintenanceOrdersLines, keys: ['Idequipamento', 'nome', 'categoriaText', 'numSerie', 'numInventario', 'numEquipamento', 'marcaText', 'servicoText', 'idRegiao'], value: search })
                                                                }, () => {
                                                                });
                                                                clearTimeout(timer);
                                                                timer = setTimeout(() => {
                                                                        this.fetchmaintenanceOrdersLines({ search: search, orderId: this.state.orderId });
                                                                }, 400);
                                                        }}
                                                        type="search"
                                                        margin="none"
                                                        endAdornment={
                                                                <InputAdornment position="end" onClick={() => { document.getElementById("oms-search").focus() }}><SearchButton round boxShadow={"none"} ><Icon search /></SearchButton></InputAdornment>
                                                        }
                                                />
                                        </SearchWrapper>
                                </Wrapper>
                                <Hr />
                                <OmDatePicker.backdrop open={this.state.datePickerOpen} />

                                {this.state.listContainerStyle.marginTop && !isLoading /*&& !isSearching*/ ?
                                        <ListContainer ref={el => this.listContainer = el} style={{ ...this.state.listContainerStyle }} onScroll={this.handleGridScroll} >
                                                <div style={{ height: '100%', width: '100%', textAlign: 'center', position: 'absolute', zIndex: 1 }} className={isLoading || maintenanceOrdersLinesIsLoading ? "" : "hidden"}>
                                                        <CircularProgress style={{ position: 'relative', top: '40%', color: _theme.palette.secondary.default }} />
                                                </div>
                                                {/* {console.log(this.state.maintenanceOrdersLines)} */}
                                                <TGrid
                                                        rows={this.state.maintenanceOrdersLines}
                                                        columns={columns}
                                                //  getRowId={getRowId}
                                                >
                                                        <BoldTypeProvider for={['nome']} />
                                                        <SortingState onSortingChange={this.handleGridScroll} />
                                                        <SelectionState />
                                                        <GroupingState expandedGroups={this.state.defaultExpandedGroups} grouping={this.state.group} onGroupingChange={(args) => {
                                                                this.setState({ group: args, defaultExpandedGroups: getDefaultExpandedGroups(this.state.maintenanceOrdersLines, args) });
                                                        }}
                                                                columnExtensions={this.state.groupingStateColumnExtensions}
                                                        />
                                                        <SearchState
                                                        // value={this.state.searchValue}
                                                        // defaultValue={this.state.searchValue}
                                                        />

                                                        <IntegratedFiltering />
                                                        <IntegratedSorting />
                                                        <IntegratedSelection />
                                                        <IntegratedGrouping />
                                                        <DragDropProvider />

                                                        {/* {urlParams.get('skip') != null && */}
                                                        <VirtualTableState
                                                                infiniteScrolling
                                                                loading={this.state.maintenanceOrdersLinesIsLoading}
                                                                totalRowCount={this.state.maintenanceOrdersTotal}
                                                                pageSize={this.state.maintenanceOrdersLines.length}
                                                                skip={this.state.skip}
                                                                getRows={this.fetchNext}

                                                        />
                                                        {/* } */}

                                                        <VirtualTable

                                                                tableComponent={props => {
                                                                        return (<VirtualTable.Table {...props} />);
                                                                }}
                                                                estimatedRowHeight={56}
                                                                stubRowComponent={(props) => {
                                                                        return (<VirtualTable.StubRow {...props} />);
                                                                }}
                                                                //height="auto"
                                                                columnExtensions={tableColumnExtensions}
                                                                rowComponent={(props) => {
                                                                        return <VirtualTable.Row {...props} className="table--row--hoverable" />
                                                                }}
                                                                cellComponent={(props) => {
                                                                        return (<MuiTableCell {..._.omit(props, ['tableRow', 'tableColumn'])}
                                                                                style={{
                                                                                        paddingLeft: (props.column.columnName == firstColumn.columnName ? '30px' : '8px'),
                                                                                        paddingRight: '8px',
                                                                                        paddingTop: '16px', paddingBottom: '15px',
                                                                                        borderColor: this.props.theme.palette.primary.keylines,
                                                                                        borderWidth: '1px',
                                                                                        color: this.props.theme.palette.primary.default,
                                                                                        whiteSpace: "nowrap",
                                                                                        overflow: 'hidden',
                                                                                        textOverflow: 'ellipsis'
                                                                                }}
                                                                        >{props.column.dataType == "bold" ?
                                                                                <Text b style={{ color: this.props.theme.palette.primary.default }}
                                                                                        data-html={true} data-tip={renderToString(
                                                                                                <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={props.value || ""}></Highlighter>
                                                                                        )}
                                                                                >
                                                                                        <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={props.value || ""}></Highlighter>
                                                                                </Text>
                                                                                :
                                                                                <Text p style={{ color: this.props.theme.palette.primary.default }}
                                                                                        data-html={true} data-tip={renderToString(
                                                                                                <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={props.value || ""}></Highlighter>
                                                                                        )}
                                                                                >
                                                                                        <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={props.value || ""}></Highlighter>
                                                                                </Text>
                                                                                }
                                                                        </MuiTableCell>)
                                                                }}
                                                        />

                                                        <TableHeaderRow
                                                                titleComponent={(props) => {
                                                                        return <Text label {...props} style={{ fontWeight: 500, marginTop: '6px' }} title="" data-tip={props.children} >{props.children}</Text>
                                                                }}
                                                                sortLabelComponent={(props) => {
                                                                        if (props.column.sortingEnabled == false) {
                                                                                return '';
                                                                        }
                                                                        return <TableHeaderRow.SortLabel  {...props} getMessage={() => ""} />
                                                                }}
                                                                rowComponent={(props) => {

                                                                        return (<TableRow {..._.omit(props, ['tableRow'])} key={props.tableRow.key} onMouseOver={() => ""} style={{ background: this.props.theme.palette.bg.grey }} />)
                                                                }}
                                                                cellComponent={(props) => {

                                                                        return (<TableHeaderRow.Cell {...props}
                                                                                style={{
                                                                                        paddingLeft: props.groupingEnabled ? (props.column.columnName == firstColumn.columnName ? '48px' : '24px') : (props.column.columnName == firstColumn.columnName ? '28px' : '8px'),
                                                                                        position: 'relative',
                                                                                }} className={(props.column.columnName == firstColumn.columnName ? 'first-cell' : '') +
                                                                                        (props.groupingEnabled ? 'grouping-enabled' : '')} />)
                                                                }}
                                                                groupButtonComponent={(props) => {
                                                                        if (props.disabled)
                                                                                return '';
                                                                        return (<Icon observation onClick={props.onGroup}
                                                                                style={{ position: 'absolute', left: 0, paddingLeft: '0', paddingBottom: '2px', fontSize: '22px' }} />)
                                                                }}

                                                                showGroupingControls showSortingControls
                                                        />
                                                        <TableGroupRow indentColumnWidth={1} showColumnsWhenGrouped={false} rowComponent={(props) => {
                                                                var lastGroup = this.state.group[this.state.group.length - 1];
                                                                if (lastGroup.columnName != props.row.groupedBy) { return (<tr></tr>); }
                                                                var values = props.row.compoundKey.split('|');
                                                                props.row.value = props.row.value;
                                                                if (values.length == 1 && values[0] == "undefined") {
                                                                        values[0] = " ";
                                                                }
                                                                // console.log('IMPIMP', props);
                                                                return (<TableRow {..._.omit(props, ['tableRow'])} _key={props.row.compoundKey} style={{
                                                                        background: this.props.theme.palette.primary.default,
                                                                        paddingLeft: '8px', paddingRight: '8px',
                                                                        paddingTop: '16px', paddingBottom: '15px'
                                                                }}>
                                                                        <MuiTableCell colSpan={props.children.length} _key={props.row.compoundKey}
                                                                                style={{
                                                                                        paddingLeft: '30px', paddingRight: '8px',
                                                                                        paddingTop: '16px', paddingBottom: '15px',
                                                                                        color: 'white'
                                                                                }}
                                                                        >
                                                                                <Wrapper inline style={{ verticalAlign: 'middle', opacity: 0.5 }} padding="0 15px 0 0"><Icon equipamentos /></Wrapper>
                                                                                {values.map((value, index) => {
                                                                                        return (<span >{index > 0 ? <Icon arrow-right style={{ color: 'white', verticalAlign: 'middle', margin: '0 6px' }} /> : ''}

                                                                                                <Text b key={index} style={{ color: 'white', verticalAlign: 'middle' }}
                                                                                                        data-html={true} data-tip={renderToString(
                                                                                                                <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={value}></Highlighter>
                                                                                                        )}
                                                                                                >
                                                                                                        <Highlighter searchWords={this.state.searchValue.split(" ")} autoEscape={true} textToHighlight={value}></Highlighter>
                                                                                                </Text></span>);
                                                                                })}
                                                                                <Wrapper inline style={{ verticalAlign: 'middle', float: 'right', width: '60px', textAlign: 'center', cursor: 'pointer' }} ><Icon open /></Wrapper>
                                                                        </MuiTableCell>
                                                                </TableRow>)
                                                        }} />
                                                        {/* <CustomGrouping children={(e) => { return e; }} /> */}
                                                        <Toolbar />
                                                        <GroupingPanel showSortingControls showGroupingControls />
                                                        <TableColumnVisibility hiddenColumnNames={this.state.hiddenColumns} onHiddenColumnNamesChange={(value) => {
                                                                this.setState({ hiddenColumns: value });
                                                        }}

                                                        />
                                                        <TableColumnReordering defaultOrder={columns.map(column => column.name)} />
                                                        <ColumnChooser toggleButtonComponent={(props) => {
                                                                return (<Button round style={{
                                                                        top: '60px',
                                                                        'zIndex': 1000,
                                                                        background: 'transparent',
                                                                        boxShadow: 'none',
                                                                        right: '-7px'
                                                                }} {..._.omit(props, ['getMessage', 'active'])} onClick={props.onToggle}><Icon row-menu /></Button>);
                                                        }}
                                                                itemComponent={(props) => {
                                                                        if (props.item.column.selectionEnabled == false) {
                                                                                return '';
                                                                        };
                                                                        return <ColumnChooser.Item {...props} />
                                                                }}
                                                        />
                                                        <RowDetailState defaultExpandedRowIds={true} />
                                                        {/* <SearchPanel /> */}
                                                </TGrid>
                                                {}
                                                <Tooltip.Hidden id={'oms-tooltip'} />

                                        </ListContainer>
                                        : <div></div>
                                }
                        </PageTemplate>
                )
        }
}

function multiGroupBy(array, group) {
        if (!group) {
                return array;
        }
        var currGrouping = _.groupBy(array, group);
        var restGroups = Array.prototype.slice.call(arguments);
        restGroups.splice(0, 2);
        if (!restGroups.length) {
                return currGrouping;
        }
        return _.transform(currGrouping, function (result, value, key) {
                result[key] = multiGroupBy.apply(null, [value].concat(restGroups));
        }, {});
}

var getDefaultExpandedGroups = (maintenanceOrdersLines, groups) => {
        var defaultExpandedGroups = [];
        var groupedList = multiGroupBy(maintenanceOrdersLines, ...groups.map((item) => { return item.columnName }));

        (function formatRecursively(list, referenceList, prefix) {
                if (prefix) { prefix = prefix + '|'; } else { prefix = ""; }
                var keys = _.keys(list);
                keys.map((item) => {
                        var listItem = list[item];
                        if (_.isObject(listItem) && !_.isArray(listItem)) { formatRecursively(listItem, referenceList, `${prefix}${item}`); }
                        item = `${prefix}${item}`;
                        referenceList.push(item);
                });
        })(groupedList, defaultExpandedGroups);

        return defaultExpandedGroups;
}
function toTitleCase(str) {
        if (!str) {
                return str;
        }
        return str.replace(
                /\w\S*/g,
                function (txt) {
                        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
                }
        );
}
const mapStateToProps = state => ({
        ...state
})
const mapDispatchToProps = dispatch => ({
        dispatchState: (payload) => dispatch({
                type: "SET_STATE",
                payload: payload
        })
})
export default withTheme(withRouter(OrdensDeManutencaoLine));