import {IMetadataBase} from "./IMetadataBase";

export interface IMetadataMachine extends IMetadataBase {
	serialNumber: string;
	dateInService: string;
	lastMaintenanceDate: string;
}