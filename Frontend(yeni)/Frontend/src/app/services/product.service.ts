import { Injectable } from '@angular/core';
import { Product } from '../model/productDetail';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  getProducts(): Promise<any[]> {
    // Ürünleri getiren işlemleri burada yapın
    // Örneğin, bir HTTP isteği kullanarak bir API'den ürünleri çekebilirsiniz.
    return new Promise<any[]>((resolve, reject) => {
      // İsteği yapın ve veriyi çekin
      // Başarılı olursa resolve ile döndürün, aksi takdirde reject ile hata döndürün
    });


  }

  getProductsMini(): Promise<Product[]> {
    // Ürünleri getiren işlemleri burada yapın ve bir Promise<Product[]> döndürün
    return this.http.get<Product[]>('API_URL')
      .toPromise()
      .then((data: any) => data as Product[])
      .catch((error) => {
        console.error('Ürünleri alma hatası:', error);
        return [] as Product[]; // Hata durumunda boş bir dizi döndürün
      });
  }
}
