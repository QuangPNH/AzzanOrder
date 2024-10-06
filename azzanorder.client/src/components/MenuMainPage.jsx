import GridLayout from "react-grid-layout";
import React, { useState } from 'react';
import '/node_modules/react-grid-layout/css/styles.css';
import '/node_modules/react-resizable/css/styles.css';
import './MenuMainPage.css';
import ProductCard from './ProductCard/ProductCard';
import MenuButton from './MenuButton/MenuComponent';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import WarningWindow from './WarningWindow/MainWindow';

import { Responsive, WidthProvider } from "react-grid-layout";

const ResponsiveGridLayout = WidthProvider(Responsive);
class MyFirstGrid extends React.Component {
    render() {
        var layoutConfig = [
            { i: 'item1', x: 0, y: 0, w: 1, h: 6, static: true },
            { i: 'item2', x: 1, y: 0, w: 1, h: 6, static: true },
            { i: 'item3', x: 0, y: 1, w: 1, h: 6, static: true },
            { i: 'item4', x: 1, y: 1, w: 1, h: 6, static: true },
            { i: 'item5', x: 0, y: 2, w: 1, h: 6, static: true },
            { i: 'item6', x: 1, y: 2, w: 1, h: 6, static: true },
            { i: 'item7', x: 0, y: 3, w: 1, h: 6, static: true },
            { i: 'item8', x: 1, y: 3, w: 1, h: 6, static: true },
            { i: 'item9', x: 0, y: 4, w: 1, h: 6, static: true },
            { i: 'item10', x: 1, y: 4, w: 1, h: 6, static: true },
            { i: 'item11', x: 0, y: 5, w: 1, h: 6, static: true },
            { i: 'item12', x: 1, y: 5, w: 1, h: 6, static: true },
            { i: 'item13', x: 0, y: 6, w: 1, h: 6, static: true },
            { i: 'item14', x: 1, y: 6, w: 1, h: 6, static: true }


        ];
        // layout is an array of objects, see the demo for more complete usage
        return (
            <ResponsiveGridLayout className="layout"
                layouts={layoutConfig}
                breakpoints={{ lg: 1200, md: 996, sm: 768, xs: 480, xxs: 0 }}
                cols={{ lg: 12, md: 10, sm: 6, xs: 4, xxs: 2 }}>
                <div key="item1"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item2"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item3"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item4"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item5"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item7"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item8"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item9"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item10"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item11"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item12"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item13"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
                <div key="item14"><ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent" title="Product Title" price="1000" /></div>
            </ResponsiveGridLayout>
            
        );
    }
}
export default MyFirstGrid;