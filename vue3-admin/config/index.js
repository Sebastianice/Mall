export default {
  development: {
    baseUrl: '/api' // 测试接口域名
  },
  beta: {
    baseUrl: '//127.0.0.1:5050/manage-api/v1' // 测试接口域名
  },
  release: {
    baseUrl: '//127.0.0.1:5050/manage-api/v1' // 正式接口域名
  }
}