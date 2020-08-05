import {IMetadataFactory} from "./IMetadataFactory";
import {IMetadataMachine} from "./IMetadataMachine";
import {IMetadataMaintenanceLookup} from "./IMetadataMaintenanceLookup";

export interface IMetadataDto {
	metadataFactories: IMetadataFactory[];
	metadataMachines: IMetadataMachine[];
	metadataMaintenanceLookups: IMetadataMaintenanceLookup[];
}