import axios from "axios";
import scrapperRequest from "../models/scrapperRequest";

axios.defaults.withCredentials = true;

export default new class scrapperService {

baseUrl = "https://localhost:7196/Scrapper"

handleErrors(err: unknown) {
    console.log("----------------------------------");
    console.log("API error occurred");
    console.log(err);
    console.log(JSON.stringify(err));
    console.trace(); // log a stacktrace so we can identify the endpoint more easily
    console.log("----------------------------------");

    return { err: err };
  }

async postScrapper(scrapperRequest: scrapperRequest)
{
 try{
    const response = await axios.post(this.baseUrl, scrapperRequest);
    return response;
 } catch (err)
 {
    return this.handleErrors(err);
 }   
}

async getScrapperHistory()
{
   return await axios.get(this.baseUrl);
}

}