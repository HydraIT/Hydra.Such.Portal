// https://github.com/diegohaz/arc/wiki/Styling
import {
    reversePalette
} from 'styled-theme/composer'

const theme = {}

theme.palette = {
    primary: {
        default: '#323F4B',
        dark: '#52606D',
        medium: '#7B8794',
        light: '#CBD2D9',
        keylines: '#E4E7EB'
    },
    secondary: {
        default: '#F9703E',
        light: '#FFE8D9'
    },
    bg: {
        white: '#FFFFFF',
        grey: '#F5F7FA'
    },
    alert: {
        bad: '#D64545',
        good: '#57AE5B'
    },
    search: '#FCEE21'
    ,
    white: '#FFFFFF'
}

theme.reversePalette = reversePalette(theme.palette)

theme.fonts = {
    primary: "Inter, Helvetica, sans-serif",
    data: "'Open Sans Condensed Light', Helvetica, sans-serif",
}


theme.sizes = {}

theme.padding = {
    0: '0px',
    8: '8px',
    16: '16px',
    24: '24px',
}

theme.radius = {
    primary: '6px',
    round: '50%',
}

theme.shadows = {
    primary: '1px 1px 2px 0px rgba(50,63,75,0.3)'
}


export default theme