import {IMetadataBase} from "./IMetadataBase";
import {ILocation} from "./ILocation";

export interface IMetadataFactory extends IMetadataBase {
	name: string;
	location: ILocation;
	dateInService: string;
}