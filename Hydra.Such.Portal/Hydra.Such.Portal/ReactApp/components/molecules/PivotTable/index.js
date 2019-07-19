// https://github.com/diegohaz/arc/wiki/Atomic-Design
/*react-styleguide: ignore*/
import React, { Component } from 'react';
import axios from 'axios';
import CircularProgress from '@material-ui/core/CircularProgress';
import styled, { css, theme, injectGlobal, withTheme } from 'styled-components';
import _theme from '../../../themes/default';
import { Button, Text, Icon, Circle, Wrapper, OmDatePicker, CheckBox, Input, Avatars, Modal, Tooltip, Spacer, Breadcrumb } from 'components';
import moment from 'moment';
import ReactDOM from 'react-dom';
import { createMuiTheme } from '@material-ui/core/styles';
import MuiBadge from '@material-ui/core/Badge';

import MuiTableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import Color from 'color';
import Highlighter from "react-highlight-words";
import MuiInput from '@material-ui/core/Input';
import InputAdornment from '@material-ui/core/InputAdornment';
import { renderToString } from 'react-dom/server';
import MuiGrid from '@material-ui/core/Grid';
import Inbox from '@material-ui/icons/Inbox';
import BoldTypeProvider from './boldDataType';
import IsPreventiveTypeProvider from './isPreventiveDataType';
import DefaultTypeProvider from './defaultDataType';
import AvatarsTypeProvider from './avatarsDataType';
import DateTypeProvider from './dateDataType';

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
import MenuItem from '@material-ui/core/MenuItem';


axios.defaults.headers.post['Accept'] = 'application/json';
axios.defaults.headers.get['Accept'] = 'application/json';

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
	[class*="MuiToolbar-root"] {
		min-height: 48px !important;
	}
	thead {
		[class*="MuiTableRow-root"] {
			height: 40px;
		}
	}
        [class*="GroupPanelContainer"] {
		margin-top: 0 !important;
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
        .table--row {
		&--hoverable {
			cursor: pointer;
			&:hover {
				background: ${_theme.palette.bg.grey};
			}
			
			&__selected {
				background-color: ${_theme.palette.secondary.default};
				&:hover {
					background-color: ${Color(_theme.palette.secondary.default).lighten(0.05).hex().toString()};
				}
				span {
					color: white;
				}
				td {
					border-bottom-color: ${ Color(_theme.palette.secondary.default).darken(0.1).desaturate(0.05).hex().toString()} !important;
				}
			}
		}
		&--group {
			td {
				border-bottom-color: ${ Color(_theme.palette.primary.default).lighten(0.1).desaturate(0.05).hex().toString()} !important;
			}
		}
        }	
        [sortable="false"] {
		 svg {
			width: 0px;
		}
		&[class*="deletable"] {
			svg {
				width: 8px;
				z-index:10;
				opacity: 0;
			}
		} 
	}
`

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
            font-size: 28px;
        }
    }
`;

const SearchWrapper = styled.div`
    position: absolute;
    display: block;
    width: ${props => props.width || '50%'};
    right:  ${props => props.right || '0'};
    z-index: 2;
    padding: 0 25px 0;
    top: 4px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
`;

var timer = 0;

const IsPreventiveFormatter = ({ value }) => <Text b >teste</Text>;

const getRowId = row => row.numEquipamento;

const StyledBadge = styled(Text)`
	position: relative;
    	border-radius: 7px;
    	padding: 4px 8px;
    	color:${props => props.color ? props.color : props.theme.palette.primary.default};
    	text-transform: uppercase;
   	margin: 6px 5px 0;
	background: ${props => props.theme.palette.search};
    	font-size: 12px;
    	line-height: 16px;
    	display: inline-block;
`
var rowPressTimer = 0;

var resetSelection;

class eTable extends Component {
	state = {
		group: [],
		hiddenColumns: [],
		tooltipReady: false,
		lines: [],
		skip: 0,
		sort: [],
		searchValue: "",
		searchValues: [],
		isLoading: false,
		rows: [],
		total: 0,
		page: 0,
		clear: false,
		selectedRows: [],
		selectionMode: false,
		boldColumns: [],
		isPreventiveColumns: []
	}

	constructor(props) {
		super(props);
		moment.locale("pt");
		this.getInitials = this.getInitials.bind(this);
		this.handleGridScroll = this.handleGridScroll.bind(this);
		this.fetchNew = this.fetchNew.bind(this);
		this.fetchNext = this.fetchNext.bind(this);
		this.state.orderId = this.props.orderId;
		this.state.group = props.columns.filter((item) => !!item.defaultExpandedGroup).map((item) => { return { columnName: item.name } });
		this.state.isLoading = props.isLoading;
		this.state.rows = props.rows;
		this.handleRowPress = this.handleRowPress.bind(this);
		this.handleRowRelease = this.handleRowRelease.bind(this);
		this.handleSelectionEvent = this.handleSelectionEvent.bind(this);
		this.resetSelection = this.resetSelection.bind(this);
		resetSelection = this.resetSelection;
		this.state.boldColumns = props.columns.filter((item) => {
			return item.type && item.dataType == 'bold';
		}).map((item) => {
			return item.name;
		});

		this.state.isPreventiveColumns = props.columns.filter((item) => {
			return item.type && item.dataType == 'isPreventive';
		}).map((item) => {
			return item.name;
		});
	}

	fetchNew({ search, sort }) {
		this.setState({ searchValue: search, sort, page: 0, isLoading: true, rows: [], total: 0 }, () => {
			this.props.getRows({ search, sort, page: 0 });
		});
	}

	handleRowPress(props) {
		Tooltip.Hidden.hide();
		Tooltip.Hidden.rebuild();
		if (this.state.selectionMode) {
			props.row.selected = !props.row.selected;
			this.setState({ selectionMode: true, selectedRows: this.state.rows.filter((item) => item.selected) }, () => {
				this.handleSelectionEvent();
			});
		}

		rowPressTimer = setTimeout(() => {

			if (this.props.allowMultiple) {
				rowPressTimer = 0;
				props.row.selected = true;
				this.setState({ selectionMode: true, selectedRows: this.state.rows.filter((item) => item.selected) }, () => {
					this.handleSelectionEvent();
				});
			}
		}, 600);
	}

	handleSelectionEvent() {
		typeof this.props.onRowSelectionChange == 'function' ? this.props.onRowSelectionChange({ selectionMode: this.state.selectionMode, selectedRows: this.state.selectedRows }) : '';
	}

	handleRowRelease(props) {
		if (!this.state.selectionMode && rowPressTimer != 0 && typeof this.props.onRowClick == 'function') {
			this.props.onRowClick(props.row);
		}
		clearTimeout(rowPressTimer);
	}

	fetchNext() {
		this.setState({ page: this.state.page + 1, isLoading: true }, () => {
			var search = this.state.searchValues;
			if (this.state.searchValue != "") {
				search = search.concat([this.state.searchValue]);
			}
			this.props.getRows({ search: search, sort: this.state.sort, page: this.state.page });
		});
	}

	componentDidMount() {
		//window.addEventListener("resize", this.handleResize);
		typeof this.props.onRef == 'function' ? this.props.onRef(this) : '';
	}

	componentWillUnmount() {
		typeof this.props.onRef == 'function' ? this.props.onRef(undefined) : '';
	}

	handleGridScroll(e) {
		setTimeout(() => {
			Tooltip.Hidden.hide();
			Tooltip.Hidden.rebuild();
		});
	}

	componentWillReceiveProps(nextProps) {
		if (nextProps.isLoading !== this.state.isLoading) {
			this.setState({ isLoading: nextProps.isLoading });
			this.handleSelectionEvent();
		}
		if (nextProps.rows !== this.state.rows) {
			this.setState({ rows: nextProps.rows, total: nextProps.total });
		}
		if (nextProps.resetSelection) {
			this.resetSelection();
		}
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

	resetSelection() {
		this.state.rows.map((item) => item.selected = false);
		this.setState({ selectionMode: false, selectedRows: [] }, () => {
			this.handleSelectionEvent();
		});
	}

	render() {
		const { isLoading, rows } = this.state;
		var columns = this.props.columns;
		var headColumns = _.differenceBy(columns, this.state.group, 'columnName');
		headColumns = headColumns.filter((val) => {
			return this.state.hiddenColumns.indexOf(val.name) < 0;
		});
		var firstColumn = headColumns[0];

		var defaultExpandedGroups = getDefaultExpandedGroups(rows, this.state.group);

		var totalToLoad = rows.length + defaultExpandedGroups.length + this.props.pageSize;
		var totalMax = this.state.total + defaultExpandedGroups.length;
		var totalRowCount = totalToLoad;
		if (totalToLoad > totalMax) {
			totalRowCount = totalMax;
		}
		if (isLoading && rows.length == 0) {
			totalRowCount = 100;
		}
		var hiddenColumns = this.state.hiddenColumns;

		console.log(totalRowCount, totalToLoad, totalMax, defaultExpandedGroups, this.state.group);

		return (
			<div>
				<div style={{ height: '100%', width: '100%', textAlign: 'center', position: 'absolute', zIndex: 1 }} className={isLoading ? "" : "hidden"}>
					<CircularProgress style={{ position: 'relative', top: '55%', color: this.props.theme.palette.secondary.default }} />
				</div>
				{this.props.searchEnabled &&
					<div>
						<SearchWrapper width={"25%"} right={"25%"} >
							{this.state.searchValues.map((val, index) => {
								return (
									<StyledBadge span key={index} >
										{val}
										<Icon decline
											style={{
												fontSize: '12px', cursor: 'pointer', position: 'relative', top: '1px', left: '2px',
												display: (this.state.selecting ? 'none' : 'inline-block')
											}}
											onClick={(e) => {
												var searchValues = this.state.searchValues.filter((s) => s != val);
												this.setState({
													searchValues: searchValues,
													sort: this.state.sort,
													page: 0, isLoading: true,
													rows: [], total: 0, clear: true,
													selectedRows: []
												}, () => {
													this.setState({ clear: false });
													this.handleSelectionEvent();
												})
											}}
										/>
									</StyledBadge>
								)
							}).reverse()}
						</SearchWrapper>
						<SearchWrapper width={"25%"} /*style={{ display: (this.state.selectionMode ? 'none' : 'inline-block') }}*/>
							<TextField inputProps={{ autoComplete: "off" }} id="oms-search"
								onKeyUp={(e) => {
									if (e.key === 'Enter') {
										var searchValues = this.state.searchValues;
										searchValues.push(this.state.searchValue);
										e.target.value = "";
										this.setState({
											searchValue: "", searchValues: searchValues, sort: this.state.sort, page: 0,
											isLoading: true, rows: [], total: 0, clear: true, selectedRows: []
										}, () => {
											this.setState({ clear: false }, () => { });
										});
									} else {
										let search = e.target.value.toLowerCase();
										Tooltip.Hidden.hide();
										Tooltip.Hidden.rebuild();
										this.setState({
											searchValue: search, sort: this.state.sort, page: 0, isLoading: true,
											rows: [], total: 0, clear: true, selectedRows: []
										}, () => {
											this.setState({ clear: false }, () => { });
										});
									}
								}}
								type="search" margin="none"
								endAdornment={
									<InputAdornment position="end" onClick={() => { document.getElementById("oms-search").focus() }}>
										<SearchButton round boxShadow={"none"} ><Icon search /></SearchButton>
									</InputAdornment>
								}
							/>
						</SearchWrapper>
					</div>
				}
				{!this.state.clear &&
					<TGrid rows={rows} columns={columns} getRowId={(item) => item[this.props.rowId]} >

						<SortingState sorting={this.state.sort} onSortingChange={(sort) => {
							Tooltip.Hidden.hide();
							Tooltip.Hidden.rebuild();
							this.setState({ sort, page: 0, isLoading: true, rows: [], total: 0, selectedRows: [] });
						}} columnExtensions={columns} />
						<SelectionState />
						<GroupingState expandedGroups={defaultExpandedGroups} grouping={this.state.group}
							columnExtensions={columns.map((item) => { return { columnName: item.name, groupingEnabled: item.groupingEnabled } })}
							onGroupingChange={(group) => {
								this.setState({ group: group });
							}}
						/>
						<SearchState />
						<IntegratedFiltering /><IntegratedSorting /><IntegratedSelection />
						<IntegratedGrouping />

						<DragDropProvider />
						<VirtualTableState
							infiniteScrolling={false}
							loading={this.state.isLoading}
							totalRowCount={totalRowCount}
							pageSize={this.props.pageSize}
							skip={this.state.skip}
							getRows={this.fetchNext} />
						{/*  */}
						<VirtualTable
							estimatedRowHeight={56}
							height="auto"
							noDataCellComponent={(props) => {
								return <VirtualTable.NoDataCell {...props} style={{
									position: "absolute", textAlign: "center", top: "50%",
									width: "100%", border: "none", padding: "0"
								}} getMessage={() => <Text p style={{ color: this.props.theme.palette.primary.light, lineHeight: '1.4em' }}>
									<Inbox style={{ fontSize: '48px' }} /><br />Sem Dados</Text>
								} />
							}}
							columnExtensions={columns.map((item) => { return { columnName: item.name, width: item.width } })}
							rowComponent={(props) => {
								return (
									<VirtualTable.Row
										{...props}
										className={"table--row--hoverable" + (props.row.selected ? " table--row--hoverable__selected" : "")}
										onMouseDown={() => { this.handleRowPress(props) }}
										onMouseUp={() => { this.handleRowRelease(props) }}
										onTouchStart={() => { this.handleRowPress(props) }}
										onTouchEnd={() => { this.handleRowRelease(props) }}
									/>
								)
							}}
							cellComponent={(props) => {
								var value = props.value;

								return (<MuiTableCell {..._.omit(props, ['tableRow', 'tableColumn'])}
									style={{
										paddingLeft: (props.column.name == firstColumn.name ? '30px' : '12px'),
										paddingRight: '12px',
										paddingTop: '16px', paddingBottom: '15px',
										borderColor: this.props.theme.palette.primary.keylines,
										borderWidth: '1px',
										color: this.props.theme.palette.primary.default,
										whiteSpace: "nowrap",
										overflow: 'hidden',
										textOverflow: 'ellipsis'
									}}
								>{(() => {
									switch (props.column.dataType) {
										case "bold":
											return (<BoldTypeProvider value={value} searchValues={this.state.searchValues.concat([this.state.searchValue])} />)
										case "isPreventive":
											return (<IsPreventiveTypeProvider value={value} searchValues={this.state.searchValues.concat([this.state.searchValue])} />)
										case "avatars":
											return (<AvatarsTypeProvider value={value} searchValues={this.state.searchValues.concat([this.state.searchValue])} />)
										case "date":
											return (<DateTypeProvider value={value} searchValues={this.state.searchValues.concat([this.state.searchValue])} />)
										default:
											return (<DefaultTypeProvider value={value} searchValues={this.state.searchValues.concat([this.state.searchValue])} />)
									}
								})()}
								</MuiTableCell>)
							}}
						/>
						<TableHeaderRow
							titleComponent={(props) => { return <Text label {...props} style={{ fontWeight: 500, marginTop: '6px' }} title="" data-tip={props.children} >{props.children}</Text> }}
							sortLabelComponent={(props) => {
								if (props.column.sortingEnabled == false) { return <TableHeaderRow.SortLabel {...props} onSort={() => { }} getMessage={() => ""} />; }
								return <TableHeaderRow.SortLabel  {...props} getMessage={() => ""} onSort={(e) => {
									props.onSort(e);
								}} />
							}}
							rowComponent={(props) => {
								return (
									<TableRow
										{..._.omit(props, ['tableRow'])}
										onMouseOver={() => ""}
										style={{ background: this.props.theme.palette.bg.grey }}
									/>
								)
							}}
							cellComponent={(props) => {

								return (<TableHeaderRow.Cell {...props}
									style={{
										paddingLeft: props.groupingEnabled ? (props.column.name == firstColumn.name ? '48px' : '24px') : (props.column.name == firstColumn.name ? '28px' : '12px'),
										position: 'relative',
									}} className={(props.column.name == firstColumn.name ? 'first-cell' : '') +
										(props.groupingEnabled ? 'grouping-enabled' : '')} />)
							}}
							groupButtonComponent={(props) => {
								if (props.disabled) { return ''; }
								return (<Icon observation onClick={props.onGroup}
									style={{ position: 'absolute', left: 0, paddingLeft: '0', paddingBottom: '2px', fontSize: '22px' }} />)
							}}
							showGroupingControls showSortingControls
						/>

						<TableGroupRow indentColumnWidth={1} showColumnsWhenGrouped={false}
							rowComponent={(props) => {
								var lastGroup = this.state.group[this.state.group.length - 1];
								if (lastGroup.columnName != props.row.groupedBy) { return (<tr></tr>); }
								var values = props.row.compoundKey.split('|');
								props.row.value = props.row.value;
								if (values.length == 1 && values[0] == "undefined") {
									values[0] = " ";
								}
								return (<TableRow {..._.omit(props, ['tableRow'])} className={"table--row--group"} style={{
									background: this.props.theme.palette.primary.default,
									paddingLeft: '12px', paddingRight: '12px',
									paddingTop: '16px', paddingBottom: '15px'
								}}>
									<MuiTableCell colSpan={props.children.length} key={props.tableRow.key.trim()}
										style={{
											paddingLeft: '30px', paddingRight: '12px',
											paddingTop: '16px', paddingBottom: '15px',
											color: 'white'
										}}
									>
										<Wrapper inline style={{ verticalAlign: 'middle', opacity: 0.5 }} padding="0 15px 0 0"><Icon equipamentos /></Wrapper>
										{values.map((value, index) => {
											return (<span key={index + props.tableRow.key.trim()} >{index > 0 ? <Icon arrow-right style={{ color: 'white', verticalAlign: 'middle', margin: '0 6px' }} /> : ''}
												<Text b key={index} style={{ color: 'white', verticalAlign: 'middle' }}
													data-html={true} data-tip={renderToString(
														<Highlighter searchWords={this.state.searchValues} autoEscape={true} textToHighlight={value}></Highlighter>
													)}
												>
													<Highlighter searchWords={this.state.searchValues} autoEscape={true} textToHighlight={value}></Highlighter>
												</Text></span>);
										})}
										<Wrapper inline style={{ verticalAlign: 'middle', float: 'right', width: '60px', textAlign: 'center', cursor: 'pointer' }} ><Icon open /></Wrapper>
									</MuiTableCell>
								</TableRow>)
							}} />
						<Toolbar />
						{this.props.groupingEnabled &&
							<GroupingPanel
								showSortingControls showGroupingControls
								emptyMessageComponent={(props) => <GroupingPanel.EmptyMessage getMessage={() => "Arraste um cabeçalho de coluna para agrupar."} />}
								itemComponent={(props) => {
									if (!props.item.column.sortingEnabled) {
										return <GroupingPanel.Item {...props} onSort={(e) => { }} sortable={"false"} />
									}
									return <GroupingPanel.Item {...props} />
								}}
							/>
						}

						<TableColumnVisibility defaultHiddenColumnNames={hiddenColumns} onHiddenColumnNamesChange={(value) => {
							hiddenColumns = value;
						}} />
						<TableColumnReordering defaultOrder={columns.map(column => column.name)} />
						<ColumnChooser
							toggleButtonComponent={(props) => {
								return (
									<div ref={el => this.columnToggleRef = el}
										style={{ position: 'absolute', top: '48px', 'zIndex': 1000, background: 'transparent', boxShadow: 'none', right: '30px' }}>
										<Button
											style={{ position: 'realtive', 'zIndex': 1000, background: 'transparent', boxShadow: 'none' }}
											round
											{..._.omit(props, ['getMessage', 'active'])}
											onClick={() => {
												props.onToggle()
											}} >
											<Icon row-menu style={{ fontSize: '16px' }} />
										</Button>
									</div>
								);
							}}
							itemComponent={(props) => {
								if (props.item.column.selectionEnabled == false) { return ''; };
								return <MenuItem style={{ paddingLeft: '5px' }} onClick={props.onToggle} value={props.item.column.name}><CheckBox checked={!(hiddenColumns.indexOf(props.item.column.name) >= 0)} /> &nbsp; {props.item.column.title}</MenuItem>;
							}}
							overlayComponent={(props) => {
								return <ColumnChooser.Overlay {...props} onHide={(e) => {
									this.setState({ hiddenColumns });
									props.onHide(e);
								}} />
							}}
							containerComponent={(props) => {
								var InnerCard = (InnerCardProps) => <div ref={el => this.teste = el} {...InnerCardProps}><ColumnChooser.Container {...props} /></div>;
								var Card = ReactDOM.findDOMNode(this.teste) ? ReactDOM.findDOMNode(this.teste).parentNode : null;
								if (Card) {
									// var position = ReactDOM.findDOMNode(this.columnToggleRef).getBoundingClientRect();
									// Card.style.left = (position.right - Card.getBoundingClientRect().width + 17) + 'px';
									// Card.style.top = position.top + 'px';
									// Card.style.opacity = 1;
								} else {
									return <InnerCard  {...props} />;
								}
								return <InnerCard {...props} />;
							}}
						/>
						<RowDetailState defaultExpandedRowIds={true} />
					</TGrid>
				}
				{!this.state.isLoading && (() => {
					setTimeout(() => {
						Tooltip.Hidden.hide();
						Tooltip.Hidden.rebuild();
					}, 400)
				})()}
				<Tooltip.Hidden id={'oms-tooltip'} />
			</div>
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

var getDefaultExpandedGroups = (lines, groups) => {

	if (!groups || groups.length < 1) {
		return [];
	}
	var defaultExpandedGroups = [];
	var groupedList = multiGroupBy(lines, ...groups.map((item) => { return item.columnName }));

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

const mapStateToProps = state => ({
	...state
})
const mapDispatchToProps = dispatch => ({
	dispatchState: (payload) => dispatch({
		type: "SET_STATE",
		payload: payload
	})
})

export default withTheme(eTable);