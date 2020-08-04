import * as React from "react";
import {Component} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';

import './custom.css'
import {Metadata} from "./components/Metadata";
import {MachineTelemetry} from "./components/MachineTelemetry";

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Layout>
				<Route exact path='/' component={Home} />
				<Route exact path='/metadata' component={Metadata} />
				<Route exact path='/machinetelemetry' component={MachineTelemetry} />
			</Layout>
		);
	}
}
