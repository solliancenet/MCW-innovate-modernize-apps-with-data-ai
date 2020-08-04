import React from "react";
import {Table} from "reactstrap";

const isUpperCaseOrNumber = (char: string): boolean => {
	const charCode = char.charCodeAt(0);
	
	return (charCode >= 41 && charCode <= 57)
		|| (charCode >= 65 && charCode <= 90);
}

const unCamelCase = (text: string): string => {
	let result: string[] = [];
	for (let i = 0; i < text.length; i++) {
		const char = text[i];
		if (i === 0) {
			result.push(char.toUpperCase())
		}
		else if (isUpperCaseOrNumber(char)) {
			result.push(" ");
			result.push(char);
		}
		else {
			result.push(char);
		}
	}
	
	return result.join("");
}

const flattenData = (data: {[key: string]: any}, parentPrefix: string, hiddenColumns: string[] = []): {[key: string]: any} => {
	let result: {[key: string]: any} = {};
	
	for (let key in data) {
		const formattedKey = unCamelCase(key);
		const value = data[key];
		
		if (hiddenColumns.includes(key)) 
			continue;
		else if (value === null) 
			result[formattedKey] = "N/A";
		else if (typeof value === "object") 
			result = {...result, ...flattenData(value, `${formattedKey} - `)} ;
		else 
			result[parentPrefix + formattedKey] = value;
	}
	
	return result;
}

export const DynamicTable: React.FC<{data: {[key: string]: any}[], hiddenList?: string[]}> = ({data, hiddenList}) => {
	const flattenedData = data.map(d => flattenData(d, "", hiddenList));
	
	const headers = Object.keys(flattenedData[0]);
	const rows = flattenedData.map(Object.values);
	
	return <>
		<Table>
			<thead>
			<tr>
				{headers.map((h, idx) => <th key={idx}>{h}</th>)}
			</tr>
			</thead>
			<tbody>
			{rows.map((r, idx) => <tr key={idx}>
				{r.map((d, idx) => <td key={idx}>{d}</td>)}
			</tr>)}
			</tbody>
		</Table>
	</>;
}