import React from 'react';
import { API } from './API';
import './styles.css';

export default function () {
    const [keyword, setKeyword] = React.useState('');
    const [url, setUrl] = React.useState('');
    const [isKeywordValid, setKeywordValidation] = React.useState(true);
    const [isUrlValid, setUrlValidation] = React.useState(true);
    const [result, setResult] = React.useState(null);
    const [searching, setSearching] = React.useState(false);
    const [searchEngineOptions, setSearchEngineOptions] = React.useState([]);
    const [searchEngine, setSearchEngine] = React.useState('');

    function LoadSearchEngines() {
        API.LoadSearchEngines().then((result) => {
            setSearchEngine(result[0]);
            let searchEngineOptionList = [];
            result.forEach((searchEningeName) => {
                searchEngineOptionList.push(<option key={searchEningeName} value={searchEningeName}>{searchEningeName}</option>);
            });

            setSearchEngineOptions(searchEngineOptionList);
        })
    }

    function search() {
        setKeywordValidation(!!keyword)
        setUrlValidation(!!url);

        if (!!keyword && !!url) {
            setSearching(true);
            API.search(keyword, url, searchEngine).then((result) => {
                setSearching(false)
                if (result.success) {
                    if (result.searchResultMatchIndex.length > 0) {
                        setResult(result.searchResultMatchIndex.join());
                    } else {
                        setResult("0");
                    }
                } else {
                    setResult(result.errorMessage.join());
                }
            })
        }
    }


    React.useEffect(() => {
        LoadSearchEngines();
    },[]);

    return (
        <div>
            <div className={'main_box'} >
                <div className={'form_row'}>
                    <label className={'keyword_label'}>Search Engine*:</label>
                    <div style={{ display: 'inline' }}>
                        <select id="SearchEngine" className={'input'} onChange={(e) => setSearchEngine(e.target.value)} value={searchEngine}>
                            {searchEngineOptions}
                        </select>
                    </div>
                </div>
                <div className={'form_row'}>
                    <label className={'keyword_label'}>Keyword*:</label>
                    <div style={{ display: 'inline' }}>
                        <input className={'input'} onChange={(e) => setKeyword(e.target.value)} placeholder={'keyword'} />
                        {!isKeywordValid && < div className={'validation_error'}>This field is required</div>}
                    </div>
                </div>
                <div className={'form_row'}>
                    <label className={'url_label'}>URL*:</label>
                    <div style={{ display: 'inline' }}>
                        <input className={'input'} onChange={(e) => setUrl(e.target.value)} placeholder={'www.example.com'} />
                        {!isUrlValid && < div className={'validation_error'}>This field is required</div>}
                    </div>
                </div>
                <div className={'button_container'}>
                    <button disabled={searching} readOnly={searching} className={'button'} onClick={search}>{searching ? 'Searching' : 'Search'}</button>
                </div>
                {!!result && <textarea readOnly className={'result_pane'} id='resultPane' value={result} />}
            </div>
        </div >
    );

}
