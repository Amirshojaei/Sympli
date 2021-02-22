import React, { Component } from 'react';
import SearchForm from './components/SearchForm';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <SearchForm />
        );
    }
}
