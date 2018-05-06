import { Inject, Injectable } from '@angular/core';
import { Http, HttpModule } from '@angular/http';
import CustomStore from 'devextreme/data/custom_store';
import LoadOptions from 'devextreme/data/custom_store';
import CustomStoreOptions from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';

/**
 * MaritalStatus Service
 */
@Injectable()
export class MaritalStatusService
{
    store : CustomStore;

    constructor(private http: Http) 
    {   
        this.setupStore();
    }
    /**
     * Setup the store with the http methods
     */
    setupStore()
    {
        var api = AppSettings.API + "/MaritalStatus"
        var http = this.http;
        this.store = new CustomStore({
            load: (loadOptions : any) : Promise<any> =>
            {
                var params = '?';
                
                if(loadOptions.filter) {
                    params += 'filter=' + loadOptions.filter || "";
                }
                
                params += 'skip=' + loadOptions.skip || 0;
                params += '&take=' + loadOptions.take || 12;

                if(loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if(loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }
                return http.get(api + params)
                    .toPromise()
                    .then(response => {
                        var json = response.json();
                        
                        return {
                            data: json,
                            totalCount: json.length
                        }
                    })
                    .catch(error => { throw 'Data Loading Error' });
            },
            byKey: (key :any) : Promise<any> => {
                return http.get(api + "/" + key)
                    .toPromise()
                    .then(response => {
                        var json = response.json();
                        return json;
                    })
                    .catch(error => { throw 'Data Loading Error' });
            }
        });
    }
}