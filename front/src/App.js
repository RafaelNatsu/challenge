import React, { useState, useEffect, useCallback } from "react";
import axios from "axios";

import { Container, PTable, Pagination, PaginationButton, PaginationItem} from "./styles";
import ToggleSwitch  from "./components/ToggleSwitch/ToggleSwitch"
const api = axios.create({
  baseURL: "http://localhost:8080",
});

const listReducer = (state, action) => {
  switch (action.type) {
    case 'ADD_ITEM':
      action.ItemName = action.ItemName.trim();
      api.post(`/Denylist`,{
        "IpAddress": action.ItemName
      },{headers: {"Access-Control-Allow-Origin": "*"}})
      .then((resp) => {
        console.log("Foi adicionado o item de id "+resp.data.idRef)
      });
      return {
        ...state,
        list: state.list.concat({ name: action.ItemName, id: action.id }),
      };
    default:
      throw new Error();
  }
};


function App() {
  const [products, setProducts] = useState([]);
  const [total, setTotal] = useState(0);
  const [limit, setLimit] = useState(5);
  const [pages, setPages] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [listData, dispatchListData] = React.useReducer(listReducer, {
    list: [],
    isShowList: true,
  });
  const [checked, setChecked] = useState(false);
  // const [listData, dispatchListData] = useState([])

  const [ItemName, setName] = React.useState('');
  useEffect(() => {
    async function loadProducts() {
      const response = await api.get(
          (checked) ?  `/ListUrl/deny?PageNumber=${currentPage}&pageSize=${limit}`:
                        `/ListUrl?pageNumber=${currentPage}&pageSize=${limit}`,
          {
            headers: {"Access-Control-Allow-Origin": "*"}
          }
      );
      setTotal(response.data.totalRecords);
      const totalPages = response.data.totalPages;
      const arrayPages = [];
      for (let i = 1; i <= totalPages; i++) {
        arrayPages.push(i);
      }

      setPages(arrayPages);
      setProducts(response.data.data);
    }
    async function getDenyList() {
      const response = await api.get(`/DenyList`,{headers: {"Access-Control-Allow-Origin": "*"}});

      listData.list = response.data.data;
    }
    getDenyList();
    loadProducts();
  }, [currentPage, limit, total,listData, checked]);

  function handleChange(event) {
    setName(event.target.value);
  }

  function handleAdd() {
    dispatchListData({ type: 'ADD_ITEM', ItemName, id: String });

    setName('');
  }
  const limits = useCallback((e) => {
    setLimit(e.target.value);
    setCurrentPage(1);
  }, []);

  return (
      <Container>
        <h3>Tabela de endereços</h3>
        <p>quantidade por pagina:</p>
        <select onChange={limits} title="quantidade por pagina">
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="15">15</option>
          <option value="100">100</option>
        </select>
        <div>
          <p title="yes: remove os itens listados na denylist">utiliza filtro:</p>
          <ToggleSwitch id="switch" checked={checked} onChange={checked => setChecked(checked)} />
        </div>
        <PTable>
          <thead>
          <tr>
            <th>#</th>
            <th>Endereços</th>
            <th>Tipo</th>
            <th>UpTime</th>
            <th>Inserido em:</th>
            {/* <th>excluir</th> */}
          </tr>
          </thead>
          <tbody>
          {products.map((product) => (
              <tr key={product.id}>
                <td>{product.id}</td>
                <td>{product.ipAddress}</td>
                <td>{product.type}</td>
                <td>{(product.uptime)??'-'}</td>
                <td>{product.inserted}</td>
              </tr>
          ))}
          </tbody>
        </PTable>
        <Pagination>
          <PaginationButton>
            {currentPage > 1 && (
                <PaginationItem onClick={() => setCurrentPage(currentPage - 1)}>
                  Previous
                </PaginationItem>
            )}
            {pages.map((page) => (
                <PaginationItem
                    isSelect={page === currentPage}
                    key={page}
                    onClick={() => setCurrentPage(page)}
                >
                  {page}
                </PaginationItem>
            ))}
            {currentPage < pages.length && (
                <PaginationItem onClick={() => setCurrentPage(currentPage + 1)}>
                  Next
                </PaginationItem>
            )}
          </PaginationButton>
        </Pagination>
        <p>Quantidade: {total}</p>
        <hr/>
        <h3>Remover endereço:</h3>
        <AddItem
        name={ItemName}
        onChange={handleChange}
        onAdd={handleAdd}
        /><br/>
        <h3>lista de removidos:</h3>
        <List list={listData.list} />
      </Container>
  );
}


const AddItem = ({ name, onChange, onAdd }) => (
  <div>
    <input type="text" value={name} onChange={onChange} />
    <button type="button" onClick={onAdd}>
      Add
    </button>
  </div>
);

const List = ({ list }) => (
  <ul>
    {list.map((item) => (
      <li key={item.id}>{item.idRef}</li>
    ))}
  </ul>
);

export default App;