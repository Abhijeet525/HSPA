import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IProperty } from '../model/iproperty';
import { IPropertyBase } from '../model/ipropertybase';
import { Property } from '../model/property';

@Injectable({
  providedIn: 'root'
})
export class HousingService {

  jsonData: any;

constructor(private http: HttpClient) { }

getAllCities(): Observable<string[]> {
  return this.http.get<string[]>('http://localhost:43106/api/city');
}

getProperty(id: number) {
  return this.getAllProperties().pipe(
    map(propertiesArray => {
      let propertiesArrayData = propertiesArray.find(p => p.Id === id);
      if(propertiesArrayData != null && propertiesArrayData != undefined){
        return propertiesArrayData;
      }
      else{
        return new Property;
      }
    })
  );
}

  getAllProperties(SellRent?: number): Observable<Property[]> {
    return this.http.get('data/properties.json').pipe(
      map(data => {
      const propertiesArray: Array<Property> = [];
      this.jsonData = data;
      let newPropertyData = localStorage.getItem('newProp');

      if(newPropertyData!= null){
        const localProperties = JSON.parse(newPropertyData);
        if(localProperties){
            for (const id in localProperties) {
              if (SellRent) {
              if (localProperties.hasOwnProperty(id) && localProperties[id].SellRent === SellRent) {
                propertiesArray.push(localProperties[id]);
              }
            } else {
              propertiesArray.push(localProperties[id]);
            }
            }
        }
      }

      for (const id in data) {
        if (SellRent) {
          if (this.jsonData.hasOwnProperty(id) && this.jsonData[id].SellRent === SellRent) {
            propertiesArray.push(this.jsonData[id]);
          }
          } else {
            propertiesArray.push(this.jsonData[id]);
        }
      }
      return propertiesArray;
      })
    );

    return this.http.get<Property[]>('data/properties.json');
  }

  addProperty(property: Property) {
    let newProp = [property];

    // Add new property in array if newProp alreay exists in local storage
    if (localStorage.getItem('newProp')) {
      newProp = [property,
                  ...JSON.parse(localStorage.getItem('newProp') || '{}')];
    }

    localStorage.setItem('newProp', JSON.stringify(newProp));
  }

  newPropertyId() {
    if(localStorage.getItem('PID')) {
      var Id = localStorage.getItem('PID');// || '{}';
      if(Id != null){
        localStorage.setItem('PID', String(+Id + 1));
        var newId = localStorage.getItem('PID') || '{}';
        return newId;
      }
      return 0;
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
}
