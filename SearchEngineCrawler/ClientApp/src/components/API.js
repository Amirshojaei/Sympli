export class API {
    static async search(keyword, url, searchEngine) {
        const response = await fetch(`Search/Search?Keyword=${keyword}&Url=${url}&searchEngineName=${searchEngine}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        return await response.json();
    }

    static async LoadSearchEngines() {
        const response = await fetch('Search/LoadSearchEngines', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        return await response.json();
    }

}
