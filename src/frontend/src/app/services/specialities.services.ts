import { Injectable } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import { HttpClient } from '@angular/common/http';

/**
 * Specialities Service
 */
@Injectable()
export class SpecialitiesService {
    store: CustomStore;

    constructor(private http: HttpClient) {
        this.setupStore();
    }
    /**
     * Setup the store with the http methods
     */
    setupStore() {
        const api = AppSettings.API + '/Specialities'
        const http = this.http;
        this.store = new CustomStore({
            load: (loadOptions: any): Promise<any> => {
                let params = '?';

                if (loadOptions.filter) {
                    params += 'filter=' + loadOptions.filter || '';
                }

                params += 'skip=' + loadOptions.skip || 0;
                params += '&take=' + loadOptions.take || 12;

                if (loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if (loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }
                return http.get<any[]>(api + params)
                    .toPromise()
                    .then(response => {
                        return {
                            data: response,
                            totalCount: response.length
                        }
                    })
                    .catch(() => { throw new Error('Data Loading Error') });
            },
            byKey: (key: any): Promise<any> => {
                return http.get(api + '/' + key)
                    .toPromise()
                    .then(response => {
                        return response;
                    })
                    .catch(() => { throw new Error('Data Loading Error') });
            }
        });
    }
}
