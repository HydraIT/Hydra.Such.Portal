// https://github.com/diegohaz/arc/wiki/Styling
import {
    reversePalette
} from 'styled-theme/composer'

const theme = {}

theme.palette = {
    primary:{ 
        default: '#323F4B',
        dark: '#52606D',
        medium: '#7B8794',
        light: '#CBD2D9',
        keylines: '#E4E7EB'
    },
    secondary:{
        default: '#F35627', 
        light: '#FFE8D9'
    },
    bg:{
        white: '#FFFFFF', 
        grey: '#F5F7FA'
    },
    alert:{
        bad: '#D64545', 
        good: '#57AE5B'
    },
    search:'#FCEE21'
    ,
    white:'#FFFFFF'
}

theme.reversePalette = reversePalette(theme.palette)

theme.fonts = {
    primary: "'Inter var', Helvetica, sans-serif",
    data: "'Berthold Akzidenz Grotesk', Helvetica, sans-serif",
}

theme.sizes = {

}

export default theme