import {IMetadataBase} from "./IMetadataBase";

export interface IMetadataMaintenanceLookup extends IMetadataBase {
	pressure: string;
	machineTemperature: string;
	maintenanceAdjustmentRequired: string;
}